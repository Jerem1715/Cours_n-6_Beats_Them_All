using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1SM : MonoBehaviour
{
    public enum Enemy1State
    {
        IDLE,
        WALK,
        JUMPUP,
        JUMPMAX,
        JUMPDOWN,
        ATTACK1,
        ATTACK2,
        AIRATTACK1,
        AIRATTACK2,
        HURT,
        AIRHURT,
        DEATH
    }

    [SerializeField] Animator animator;
    [SerializeField] GameObject player;
    [SerializeField] float speedInit;
    [SerializeField] Vector3 startForce;
    [SerializeField] float posEnemyDetect;
    [SerializeField] float range = 3f;

    Rigidbody2D rb2D;

    Vector2 enemyDir;
    Vector2 enemyDirNormalized;

    bool isWalk = false;

    public Enemy1State currentState;

    float distancePlayerToEnemy;


    // Start is called before the first frame update
    void Start()
    {
        //On récupère le rigidbody du player
        rb2D = GetComponent<Rigidbody2D>();

        //rb2D.AddForce(startForce * speedInit, ForceMode.Impulse);

        //On applique un état de base 
        currentState = Enemy1State.IDLE;

        //On lance la fonction
        OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        enemyDir = player.transform.position - transform.position;
        enemyDirNormalized = enemyDir.normalized;

        distancePlayerToEnemy = Vector2.Distance(player.transform.position, transform.position);

        if (rb2D.velocity.x < 0)
        {
            transform.eulerAngles = new Vector2(0, -180);
        }

        
        OnStateUpdate();
    }

    private void FixedUpdate()
    {
        OnStateFixedUpdate();
    }

    void OnStateEnter()
    {
        switch (currentState)
        {
            case Enemy1State.IDLE:

                isWalk = false;

                rb2D.velocity = Vector2.zero;

                break;
            case Enemy1State.WALK:

                animator.SetBool("WALK", true);

                break;
            case Enemy1State.JUMPUP:

                animator.SetTrigger("JUMP");

                break;
            case Enemy1State.JUMPMAX:

                animator.SetTrigger("JUMPMAX");

                break;
            case Enemy1State.JUMPDOWN:

                animator.SetTrigger("JUMPDOWN");

                break;
            case Enemy1State.ATTACK1:

                animator.SetTrigger("ATTACK1");

                break;
            case Enemy1State.ATTACK2:

                animator.SetTrigger("ATTACK2");

                break;
            case Enemy1State.AIRATTACK1:

                animator.SetTrigger("AIRATTACK1");

                break;
            case Enemy1State.AIRATTACK2:

                animator.SetTrigger("AIRATTACK2");

                break;
            case Enemy1State.HURT:

                animator.SetTrigger("HURT");

                break;
            case Enemy1State.AIRHURT:

                animator.SetTrigger("AIRHURT");

                break;
            case Enemy1State.DEATH:

                //animator.SetFloat("DEATH");

                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (currentState)
        {
            case Enemy1State.IDLE:


                //To WALK
                if (distancePlayerToEnemy <= range) 
                {
                    TransitionToState(Enemy1State.WALK);
                }

               // if ((0)) //distance < x
               // {
               //     TransitionToState(Enemy1State.ATTACK1);
               // }

                break;
            case Enemy1State.WALK:

                if (distancePlayerToEnemy > range) //dirInput = Vector2.zero
                {
                    TransitionToState(Enemy1State.IDLE);
                }

                break;
            case Enemy1State.JUMPUP:

                if (rb2D.velocity.y == 0)
                {
                    TransitionToState(Enemy1State.IDLE);
                }

                break;
            case Enemy1State.JUMPMAX:

                if (rb2D.velocity.y == 0)
                {
                    TransitionToState(Enemy1State.IDLE);
                }

                break;
            case Enemy1State.JUMPDOWN:

                if (rb2D.velocity.y == 0)
                {
                    TransitionToState(Enemy1State.IDLE);
                }

                break;
            case Enemy1State.ATTACK1:



                break;
            case Enemy1State.ATTACK2:
                break;
            case Enemy1State.AIRATTACK1:
                break;
            case Enemy1State.AIRATTACK2:
                break;
            case Enemy1State.HURT:
                break;
            case Enemy1State.AIRHURT:
                break;
            case Enemy1State.DEATH:
                break;
            default:
                break;
        }
    }

    void OnStateFixedUpdate()
    {
        switch (currentState)
        {
            case Enemy1State.IDLE:
                break;
            case Enemy1State.WALK:

                if (distancePlayerToEnemy <= range)
                {
                    rb2D.velocity = enemyDirNormalized * speedInit;
                }



                break;
            case Enemy1State.JUMPUP:
                break;
            case Enemy1State.JUMPMAX:
                break;
            case Enemy1State.JUMPDOWN:
                break;
            case Enemy1State.ATTACK1:
                break;
            case Enemy1State.ATTACK2:
                break;
            case Enemy1State.AIRATTACK1:
                break;
            case Enemy1State.AIRATTACK2:
                break;
            case Enemy1State.HURT:
                break;
            case Enemy1State.AIRHURT:
                break;
            case Enemy1State.DEATH:
                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case Enemy1State.IDLE:
                break;
            case Enemy1State.WALK:

                animator.SetBool("WALK", false);

                break;
            case Enemy1State.JUMPUP:
                break;
            case Enemy1State.JUMPMAX:
                break;
            case Enemy1State.JUMPDOWN:
                break;
            case Enemy1State.ATTACK1:
                break;
            case Enemy1State.ATTACK2:
                break;
            case Enemy1State.AIRATTACK1:
                break;
            case Enemy1State.AIRATTACK2:
                break;
            case Enemy1State.HURT:
                break;
            case Enemy1State.AIRHURT:
                break;
            case Enemy1State.DEATH:
                break;
            default:
                break;
        }
    }

    void TransitionToState(Enemy1State nextState)
    {
        OnStateExit();

        currentState = nextState;

        OnStateEnter();
    }

}

//
//if (player == null)
//{
//    return;
//}
//
//if (Vector3.Distance(player.transform.position, transform.position) <= range)
//{
//    //go to player
//    Vector3 dirMove = player.transform.position - transform.position;
//
//    //change position y de l'enemie
//    Vector3 replacePosEnemy = transform.position;
//    replacePosEnemy.y = posEnemyDetect;
//
//    rb2D.velocity = dirMove.normalized * speedEnemyDetect;
//
//    transform.LookAt(player.transform.position);
//}