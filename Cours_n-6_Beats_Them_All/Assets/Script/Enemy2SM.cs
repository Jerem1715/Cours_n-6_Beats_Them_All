using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy2SM : MonoBehaviour
{
    public enum Enemy2Stat
    {
        IDLE,
        WALK,
        JUMP_DOWN, JUMP_MAX, JUMP_UP,
        ATTACK1, ATTACK2,
        AIR_ATTACK1, AIR_ATTACK2,
        HURT, //blesser
        AIR_HURT,
        THROW, //jeter
        DEATH
    }

    public Enemy2Stat currentState;
    [Header("STATE")]
    Animator animator;

    [SerializeField] GameObject graphics;


    [Header("OBJECT")]
    [SerializeField] GameObject player1;

    [Header("SPEED,RANGE")]
    [SerializeField] float walkSpeed = 2f; //scriptable ?
    [SerializeField] float jumpSpeed = 5f; //scriptable ?
    [SerializeField] float scope = 3f;

    bool isGrounded; // scriptable ?
    bool isJump;
    bool isAttack = false;
    bool isWalk = false;
    bool isOnCollision = false;
 
    float distancePlayerToEnemy;

    Rigidbody2D rb2D;
    Vector2 dirMove;
    Vector2 dirMoveNormalized;
    Vector2 currentDestination;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        animator = graphics.GetComponent<Animator>();

        currentState = Enemy2Stat.IDLE;

        currentDestination = player1.transform.position;

        OnStateEnter();
    }

    void Update()
    {

        //go to the player
        dirMove = player1.transform.position - transform.position;
        dirMoveNormalized = dirMove.normalized;

        if (rb2D.velocity.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
        }

        if (rb2D.velocity.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        OnStateUpdate();
    }

    private void FixedUpdate()
    {

        OnStateFixedUpdate();
    }

    void OnStateFixedUpdate()
    {
        distancePlayerToEnemy = Vector2.Distance(player1.transform.position, transform.position);
        switch (currentState)
        {
            case Enemy2Stat.IDLE:
                break;
            case Enemy2Stat.WALK:

                if (distancePlayerToEnemy <= scope)
                {
                    rb2D.velocity = dirMoveNormalized * walkSpeed;
                }
                break;
            case Enemy2Stat.ATTACK1:
                //collision
                break;
            case Enemy2Stat.ATTACK2:
                //collision
                break;
            case Enemy2Stat.JUMP_DOWN:

                break;
            case Enemy2Stat.JUMP_MAX:
                break;
            case Enemy2Stat.JUMP_UP:
                break;
            case Enemy2Stat.AIR_ATTACK1:
                break;
            case Enemy2Stat.AIR_ATTACK2:
                break;
            case Enemy2Stat.HURT:
                break;
            case Enemy2Stat.AIR_HURT:
                break;
            case Enemy2Stat.THROW:
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
                isAttack = true;
                animator.SetBool("ATTACK1", true);
                break;
            case Enemy2Stat.ATTACK2:
                break;
            case Enemy2Stat.AIR_ATTACK1:
                break;
            case Enemy2Stat.AIR_ATTACK2:
            case Enemy2Stat.JUMP_DOWN:
                //ref sauter bas
                break;
            case Enemy2Stat.JUMP_MAX:
                //ref sauter maximum
                break;
            case Enemy2Stat.JUMP_UP:
                //ref sauter basiq ?
                break;
                break;
            case Enemy2Stat.HURT:
                //
                break;
            case Enemy2Stat.AIR_HURT:
                break;
            case Enemy2Stat.THROW:
                //jeter
                break;
            case Enemy2Stat.DEATH:
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
                if (distancePlayerToEnemy <= scope) // dirMagnitude != 0
                {
                    TransitionToState(Enemy2Stat.WALK);
                }
                break;
            case Enemy2Stat.WALK:
                if (distancePlayerToEnemy > scope) // dirMagnitude -= 0
                {
                    TransitionToState(Enemy2Stat.IDLE);
                }

                if (distancePlayerToEnemy <= scope && isAttack == true)
                {
                    TransitionToState(Enemy2Stat.ATTACK1);
                }
                break;
            case Enemy2Stat.JUMP_DOWN:
                break;
            case Enemy2Stat.JUMP_MAX:
                break;
            case Enemy2Stat.JUMP_UP:
                break;
            case Enemy2Stat.ATTACK1:
                if (distancePlayerToEnemy <= scope/*portée*/ )
                {
                    TransitionToState(Enemy2Stat.WALK);
                }

                if (distancePlayerToEnemy > scope)
                {
                    TransitionToState(Enemy2Stat.IDLE);
                }
                break;
            case Enemy2Stat.ATTACK2:
                break;
            case Enemy2Stat.AIR_ATTACK1:
                break;
            case Enemy2Stat.AIR_ATTACK2:
                break;
            case Enemy2Stat.HURT:
                break;
            case Enemy2Stat.AIR_HURT:
                break;
            case Enemy2Stat.THROW:
                break;
            case Enemy2Stat.DEATH:
                break;
            default:
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Player"))
        {
            switch (currentState)
            {
                case Enemy2Stat.IDLE:
                    break;
                case Enemy2Stat.WALK:
                    isAttack = true;
                    break;
                case Enemy2Stat.ATTACK1:
                    break;
                default:
                    break;
            }
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
            case Enemy2Stat.JUMP_DOWN:
                break;
            case Enemy2Stat.JUMP_MAX:
                break;
            case Enemy2Stat.JUMP_UP:
                break;
            case Enemy2Stat.ATTACK1:
                isAttack = false;
                animator.SetBool("ATTACK1", false); 
                break;
            case Enemy2Stat.ATTACK2:
                break;
            case Enemy2Stat.AIR_ATTACK1:
                break;
            case Enemy2Stat.AIR_ATTACK2:
                break;
            case Enemy2Stat.HURT:
                break;
            case Enemy2Stat.AIR_HURT:
                break;
            case Enemy2Stat.THROW:
                break;
            case Enemy2Stat.DEATH:
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


    


}
