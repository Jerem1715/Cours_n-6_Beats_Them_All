using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy2SM : MonoBehaviour
{
    public enum Enemy2Stat
    {
        IDLE,
        WALK,
        ATTACK1, ATTACK2,
        HURT, //blesser
        DEATH
    }

    public Enemy2Stat currentState;
    [Header("STATE")]
    Animator animator;

    [SerializeField] GameObject graphics;


    [Header("OBJECTS")]
    [SerializeField] GameObject player1;
    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject _RecordPrefab;

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
        animator = graphics.GetComponent<Animator>();
    }
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        currentState = Enemy2Stat.IDLE;

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
            case Enemy2Stat.IDLE:
                break;
            case Enemy2Stat.WALK:
                rb2D.velocity = dirMoveNormalized * walkSpeed;
                //isDead = false;
                break;
            case Enemy2Stat.ATTACK1:
                //collision
                break;
            case Enemy2Stat.ATTACK2:
                //collision
                break;
            case Enemy2Stat.DEATH:
                break;
            default:
                break;
        }
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case Enemy2Stat.IDLE:

                rb2D.velocity = Vector2.zero;
                break;

            case Enemy2Stat.WALK:
                animator.SetBool("WALK", true);
               
                break;

            case Enemy2Stat.ATTACK1:

                attackPoint.SetActive(true); 

                rb2D.velocity = Vector2.zero;
                StartCoroutine(Attack());

                break;
            case Enemy2Stat.HURT:
                
                animator.SetTrigger("HURT");
                break;
            
            case Enemy2Stat.ATTACK2:

                attackPoint.SetActive(true);
                break;

            case Enemy2Stat.DEATH:

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
            case Enemy2Stat.IDLE:

                if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange/* && isDead == false*/)
                {
                    TransitionToState(Enemy2Stat.WALK);
                }
                if (distanceToPlayer <= attackRange)
                    TransitionToState(Enemy2Stat.ATTACK1);

                break;
            case Enemy2Stat.WALK:

                if (distanceToPlayer <= attackRange)
                    TransitionToState(Enemy2Stat.ATTACK1);

                #region condition de transition vers attack 2
                //if (distanceToPlayer <= attackRange && RandomAttack() == 1)
                //    TransitionToState(BossState.ATTACK2);
                #endregion

                if (distanceToPlayer >= detectionRadius)
                    TransitionToState(Enemy2Stat.IDLE);

                break;
            case Enemy2Stat.ATTACK1:

                #region condition de transition vers Attack2
                //if (distanceToPlayer <= attackRange && RandomAttack() == 1)
                //    TransitionToState(BossState.ATTACK2);
                #endregion

                if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange /*&& isDead ==false*/)
                    TransitionToState(Enemy2Stat.WALK);

                if (distanceToPlayer > detectionRadius)
                    TransitionToState(Enemy2Stat.IDLE);

                break;
            case Enemy2Stat.ATTACK2:

                #region ATTACK2 impl?menter mais non fonction
                //if (distanceToPlayer <= attackRange && RandomAttack() == 0)
                //    TransitionToState(BossState.ATTACK1);

                //if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange)
                //    TransitionToState(BossState.WALK);

                //if (distanceToPlayer > detectionRadius)
                //    TransitionToState(BossState.IDLE);
                #endregion
                break;
            case Enemy2Stat.HURT:
                Debug.Log("Mon entr?e de Hurt");
                   
                break;
            case Enemy2Stat.DEATH:
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
            case Enemy2Stat.IDLE:
                break;
            case Enemy2Stat.WALK:
                animator.SetBool("WALK", false);
                break;
            case Enemy2Stat.ATTACK1:

                attackPoint.SetActive(false);

                //canAttack = false;
                break;
            case Enemy2Stat.ATTACK2:

                attackPoint.SetActive(false);

                break;
            case Enemy2Stat.HURT:
                break;
            case Enemy2Stat.DEATH:
                //isDead=false;
                animator.SetBool("DEATH", false);
                break;
            default:
                break;
        }
    }

    void TransitionToState(Enemy2Stat nextState)
    {
        OnStateExit();

        currentState = nextState;

        OnStateEnter();
    }

    private void OnDrawGizmosSelected()
    {
        //zone de d?tection de l'enemy
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
        
        attackPoint.SetActive(false);

        //attend 2 second
        yield return new WaitForSeconds(3f);
       
        // d?truit l"objet 
        Destroy(gameObject);

        //? la supp de l'objet, tu cr?e un collectible Disk
        Instantiate(_RecordPrefab, transform.position, Quaternion.identity);
    }
    IEnumerator Attack()
    {
        canAttack = false;
        //animator.SetBool("ATTACK1", true);
        animator.SetTrigger("ATTACK");
        yield return new WaitForSeconds(attackDelay);
        TransitionToState(Enemy2Stat.IDLE);
        canAttack = true;
    }



    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            #region sans Coroutine
            //step1
            //animator.SetTrigger("DEATH");
            //Destroy(gameObject);
            #endregion

            //step2 avec coroutine
            StartCoroutine(DestroyEnemy());
        }
        else
        {
            animator.SetBool("HURT",true);
            //Debug.Log(health);
        }
    }


    #region fonction al?atoire qui g?re les attack 1 et 2
    //public float RandomAttack()
    //{
    //    float randomAttack = UnityEngine.Random.Range(0, 2);
    //    Debug.Log(randomAttack);
    //    return randomAttack;
    //}
    #endregion

}
