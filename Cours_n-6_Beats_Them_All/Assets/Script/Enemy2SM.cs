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


    [Header("OBJECT")]
    [SerializeField] GameObject player1;
    [SerializeField] GameObject attackPoint;
    [SerializeField] GameObject _DiskPrefab;

    [Header("LAYERS")]
    [SerializeField] LayerMask playerLayer;

    [Header("SPEED,RANGE")]
    [SerializeField] float walkSpeed = 2f; 
    [SerializeField] float detectionRadius = 3f;
    [SerializeField] float attackDelay = 0.75f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] int health = 3;
    [SerializeField] int score = 750;
    [SerializeField] int damageAmount = 1;
    float distanceToPlayer;


    [Header("Booleans")]
    bool canAttack = true;
    bool isInAttack1 = true;

    Collision2D collisionPlayer;
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

                rb2D.velocity = Vector2.zero;
                StartCoroutine(Attack());

                break;
            case Enemy2Stat.HURT:

                animator.SetTrigger("HURT");
                break;
            case Enemy2Stat.ATTACK2:
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
                if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange)
                    TransitionToState(Enemy2Stat.WALK);

                if (distanceToPlayer <= attackRange)
                    TransitionToState(Enemy2Stat.ATTACK1);

                break;
            case Enemy2Stat.WALK:

                if (distanceToPlayer <= attackRange)
                    TransitionToState(Enemy2Stat.ATTACK1);

                //if (distanceToPlayer <= attackRange && RandomAttack() == 1)
                //    TransitionToState(Enemy2Stat.ATTACK2);

                if (distanceToPlayer >= detectionRadius)
                    TransitionToState(Enemy2Stat.IDLE);

                break;
            case Enemy2Stat.ATTACK1:

                //if (distanceToPlayer <= attackRange && RandomAttack() == 1)
                //    TransitionToState(Enemy2Stat.ATTACK2);

                if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange)
                    TransitionToState(Enemy2Stat.WALK);

                if (distanceToPlayer > detectionRadius)
                    TransitionToState(Enemy2Stat.IDLE);

                break;
            case Enemy2Stat.ATTACK2:

                #region ATTACK2 implémenter mais non fonction
                //if (distanceToPlayer <= attackRange && RandomAttack() == 0)
                //    TransitionToState(Enemy2Stat.ATTACK1);

                //if (distanceToPlayer <= detectionRadius && distanceToPlayer > attackRange)
                //    TransitionToState(Enemy2Stat.WALK);

                //if (distanceToPlayer > detectionRadius)
                //    TransitionToState(Enemy2Stat.IDLE);
                #endregion
                break;
            case Enemy2Stat.HURT:
                Debug.Log("Mon entrée de Hurt");
                   
                break;
            case Enemy2Stat.DEATH:
               
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
                //canAttack = false;
                break;
            case Enemy2Stat.ATTACK2:

                break;
            case Enemy2Stat.HURT:
                break;
            case Enemy2Stat.DEATH:
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }

    IEnumerator DestroyEnemy()
    {
        //joue l'anim Death
        animator.SetBool("DEATH", true);

        //attend 2 second
        yield return new WaitForSeconds(2f);

        // détruit l"objet 
        Destroy(gameObject);

        //à la supp de l'objet, tu crée un collectible Disk
        Instantiate(_DiskPrefab, transform.position, Quaternion.identity);
    }
    IEnumerator Attack()
    {
        canAttack = false;
        //animator.SetBool("ATTACK1", true);
        animator.SetTrigger("ATTACK");
        yield return new WaitForSeconds(attackDelay);
        TransitionToState(Enemy2Stat.IDLE);
        canAttack = true;

        #region logique AVEC ATTACK2
        //if (RandomAttack() == 0)
        //{
        //canAttack = false;
        ////animator.SetBool("ATTACK1", true);
        //animator.SetTrigger("ATTACK");
        //yield return new WaitForSeconds(attackDelay);
        //TransitionToState(Enemy2Stat.IDLE);
        //canAttack = true;
        //}

        //if (RandomAttack() == 1)
        //{
        //    canAttack = false;
        //    animator.SetTrigger("ATTACK2");
        //    yield return new WaitForSeconds(attackDelay);

        //    TransitionToState(Enemy2Stat.IDLE);
        //    canAttack = true;
        //}
        #endregion
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            //step1
            //animator.SetTrigger("DEATH");
            //Destroy(gameObject);

            //step2 avec coroutine
            StartCoroutine(DestroyEnemy());

        }
        else
        {
            animator.SetBool("HURT",true);
        }
    }

  
        //public void AddScore(int amount) // amount => montant 
        //{
        //    score += amount;

        //    //scoreUI.text = "score: " + score.ToString("d5");
        //    ////lz même opération
        //    scoreUI.text = $"score: {score.ToString("d5")}";
        //}
   

    #region fonction aléatoire qui gère les attack 1 et 2
    //public float RandomAttack()
    //{
    //    float randomAttack = UnityEngine.Random.Range(0, 2);
    //    Debug.Log(randomAttack);
    //    return randomAttack;
    //}
    #endregion

}
