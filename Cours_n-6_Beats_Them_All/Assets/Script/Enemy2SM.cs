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

    [SerializeField]GameObject graphics;
    

    [Header("OBJECT")]
    [SerializeField] GameObject player1;

    [Header("SPEED AND RANGE")]
    [SerializeField] float walkSpeed = 2f; //scriptable ?
    [SerializeField] float range = 3f;


    bool isGrounded; // scriptable ?
    bool isJump;
    bool isWalk = false;



    Rigidbody2D rb2D;
    Vector2 dirMove; 
    Vector2 dirMoveNormalized;
    float distancePlayerToEnemy;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
        animator = graphics.GetComponent<Animator>();
        
        currentState = Enemy2Stat.IDLE;

        OnStateEnter();
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

            case Enemy2Stat.JUMP_DOWN:
                //ref sauter bas
                
                /*
                 
                 */
                break;
            case Enemy2Stat.JUMP_MAX:
                //ref sauter maximum
                break;
            case Enemy2Stat.JUMP_UP:
                //ref sauter basiq ?
                break;
            case Enemy2Stat.ATTACK1:
                break;
            case Enemy2Stat.ATTACK2:
                break;
            case Enemy2Stat.AIR_ATTACK1:
                break;
            case Enemy2Stat.AIR_ATTACK2:
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

    // Update is called once per frame
    void Update()
    {

        //go to the player
        dirMove = player1.transform.position - transform.position;
        dirMoveNormalized = dirMove.normalized;

        if (rb2D.velocity.x < 0)
        {
            transform.eulerAngles = new Vector2(0,-180);
        }

        //pas besoin
        //if (rb2D.velocity.x > 0)
        //{
        //    transform.eulerAngles = new Vector2(0, 180);
        //}

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

                if (distancePlayerToEnemy <= range)
                {
                    rb2D.velocity = dirMoveNormalized * walkSpeed;
                }
                break;
            case Enemy2Stat.JUMP_DOWN:
                
                break;
            case Enemy2Stat.JUMP_MAX:
                break;
            case Enemy2Stat.JUMP_UP:
                break;
            case Enemy2Stat.ATTACK1:
                //collision
                break;
            case Enemy2Stat.ATTACK2:
                //collision
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

    void OnStateUpdate()
    {
        switch (currentState)
        {
            case Enemy2Stat.IDLE:
                if (distancePlayerToEnemy <= range) // dirMagnitude != 0
                {
                    TransitionToState(Enemy2Stat.WALK);
                }

                //jump, Attack => ceux sont plutot des action
                break;
            case Enemy2Stat.WALK:
                if (distancePlayerToEnemy > range) // dirMagnitude -= 0
                {
                    TransitionToState(Enemy2Stat.IDLE);
                }
                break;
            case Enemy2Stat.JUMP_DOWN:
                break;
            case Enemy2Stat.JUMP_MAX:
                break;
            case Enemy2Stat.JUMP_UP:
                break;
            case Enemy2Stat.ATTACK1:
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

    //lorsque l'object E rentre en collision 
    //    si collision.gameObject.tag=="player"
    //        switch
    //            currentState



}
