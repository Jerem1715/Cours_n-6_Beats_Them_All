using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSM : MonoBehaviour
{
    public enum BossState
    {
        IDLE,
        WALK,
        ATTACK,
        SLAM, //claquer fortement
        TAUNT,//raillerie
        HURT, //blesser
        DEATH
    }

    public BossState currentState;
    [Header("STATE")]
    Animator animator;

    [Header("OBJECTS")]
    [SerializeField] GameObject player1;
    [SerializeField] GameObject attackPoint;
    //[SerializeField] GameObject _RecordPrefab;

    [Header("SPEED,RANGE")]
    [SerializeField] float walkSpeed = 2f;
    [SerializeField] float detectionRadius = 3f;
    [SerializeField] float attackDelay = 1f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] int health = 3;
    [SerializeField] int score = 750;
    float distanceToPlayer;


    [Header("Booleans")]
    bool canAttack = true;
    bool isDead = true;
    //bool isInAttack1 = true;

    Rigidbody2D rb2D;
    Vector2 dirToPlayer;
    Vector2 dirMoveNormalized;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        currentState = BossState.IDLE;

        attackPoint.SetActive(false);

        OnStateEnter();
    }

    void Update()
    {
        dirToPlayer = player1.transform.position - transform.position;
        dirMoveNormalized = dirToPlayer.normalized;

        Orientation();

        OnStateUpdate();
    }

    private void Orientation()
    {
        if (rb2D.velocity.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
        }

        if (rb2D.velocity.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        OnStateFixedUpdate();
    }

    void OnStateFixedUpdate()
    {
        distanceToPlayer = Vector2.Distance(player1.transform.position, transform.position);

        switch (currentState)
        {
            case BossState.IDLE:
                break;
            case BossState.WALK:
                rb2D.velocity = dirMoveNormalized * walkSpeed;
                break;
            case BossState.ATTACK:
                break;
            case BossState.SLAM:
                break;
            case BossState.TAUNT:
                break;
            case BossState.HURT:
                break;
            case BossState.DEATH:
                break;
            default:
                break;
        }
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case BossState.IDLE:

                rb2D.velocity = Vector2.zero;
                break;

            case BossState.WALK:

                animator.SetBool("WALKING", true);
                break;

            case BossState.ATTACK:

                attackPoint.SetActive(true);

                rb2D.velocity = Vector2.zero;
                StartCoroutine(Attack());

                break;
            case BossState.HURT:

                //animator.SetTrigger("HURT");
                break;

            case BossState.SLAM:
                break;
            case BossState.TAUNT:
                break;
            case BossState.DEATH:

                rb2D.velocity = Vector2.zero;
                StartCoroutine(DestroyEnemy());

                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (currentState)
        {
            case BossState.IDLE:

                if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange)
                    TransitionToState(BossState.WALK);

                break;
            case BossState.WALK:

                if (distanceToPlayer <= attackRange)
                    TransitionToState(BossState.ATTACK);


                if (distanceToPlayer >= detectionRadius)
                    TransitionToState(BossState.IDLE);

                break;
            case BossState.ATTACK:

                if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange )
                    TransitionToState(BossState.WALK);

                if (distanceToPlayer > detectionRadius)
                    TransitionToState(BossState.IDLE);

                break;
            case BossState.SLAM:
                break;
            case BossState.TAUNT:
                break;
            case BossState.HURT:
                Debug.Log("Mon entrée de Hurt");

                break;
            case BossState.DEATH:
                isDead = true;
                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case BossState.IDLE:
                break;
            case BossState.WALK:
                animator.SetBool("WALKING", false);
                break;
            case BossState.ATTACK:

                attackPoint.SetActive(false);
                break;
            case BossState.SLAM:
                break;
            case BossState.TAUNT:
                break;
            case BossState.HURT:
                break;
            case BossState.DEATH:
                animator.SetBool("DEATH", false);
                break;
            default:
                break;
        }
    }

    void TransitionToState(BossState nextState)
    {
        OnStateExit();

        currentState = nextState;

        OnStateEnter();
    }

    private void OnDrawGizmosSelected()
    {
        //zone de détection de l'enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        //zone d'attack
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator DestroyEnemy()
    {

        //joue l'anim Death
        animator.SetBool("DEATH", true);

        //attackPoint.SetActive(false);

        //attend 2 second
        yield return new WaitForSeconds(3f);

        // détruit l"objet 
        Destroy(gameObject);

        //à la supp de l'objet, tu crée un collectible Disk
        //Instantiate(_RecordPrefab, transform.position, Quaternion.identity);
    }
    IEnumerator Attack()
    {
        canAttack = false;
        //animator.SetBool("ATTACK1", true);
        animator.SetTrigger("ATTACKING");
        yield return new WaitForSeconds(attackDelay);
        TransitionToState(BossState.IDLE);
        canAttack = true;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            StartCoroutine(DestroyEnemy());
        }
        else
        {
            animator.SetBool("HURT", true);
            Debug.Log(health);
        }
    }
}
