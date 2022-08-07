using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    public enum Player1State
    {
        IDLE,
        WALK,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        ATTACK4,
        SPRINT,
        JUMPUP,
        JUMPMAX,
        JUMPDOWN,
        JUMPLAND,
        AIRATTACK1,
        AIRATTACK2,
        AIRATTACK3,
        AIRATTACK4,
        DIVE,
        GROUNDPOUND,
        HURT,
        AIRHURT,
        CANPICKUP,
        CANIDLE,
        CANWALK,
        CANJUMPUP,
        CANJUMPMAX,
        CANJUMPDOWN,
        CANJUMPLAND,
        CANSPRINT,
        DEATH,
        VICTORY,
        TAUNT,
        THROW,
        SPECIAL,
        CHARACTERSELECT
    }


    [SerializeField] Animator animator;
    [SerializeField] float speed;
    [SerializeField] float sprintSpeed;

    Vector2 dirInput;


    Rigidbody2D rb2D;

    public Player1State currentState;


    //Saut
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpDuration = 2f;
    Transform graphics;
    float jumpTimer;


    //Awake is called before Start
    private void Awake()
    {
        //Permet de récupérer la position de l'enfant "GraphicsP1" du Player afin de préparer le saut
        graphics = transform.Find("GraphicsP1");
    }


    // Start is called before the first frame update
    void Start()
    {
        //On récupèr ele rigidbody2d du player
        rb2D = GetComponent<Rigidbody2D>();

        //On applique un état de base 
        currentState = Player1State.IDLE;

        //On lance la fonction
        OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        //On récupère les inputs
        dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // //On ne fait une animation que si on applique une direction en appuyant sur une touche
        // if (dirInput != Vector2.zero)
        // {
        //     animator.SetFloat("InputX", dirInput.x);
        //     animator.SetFloat("InputY", dirInput.y);
        // }

        //Permet aux sprites de se retourner à 180°
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

    void OnStateEnter()
    {
        switch (currentState)
        {
            case Player1State.IDLE:

                rb2D.velocity = Vector2.zero;

                break;
            case Player1State.WALK:

                animator.SetBool("WALK", true);

                break;
            case Player1State.ATTACK1:
                break;
            case Player1State.ATTACK2:
                break;
            case Player1State.ATTACK3:
                break;
            case Player1State.ATTACK4:
                break;
            case Player1State.SPRINT:

                animator.SetBool("SPRINT", true);

                break;
            case Player1State.JUMPUP:
                break;
            case Player1State.JUMPMAX:
                break;
            case Player1State.JUMPDOWN:
                break;
            case Player1State.JUMPLAND:
                break;
            case Player1State.AIRATTACK1:
                break;
            case Player1State.AIRATTACK2:
                break;
            case Player1State.AIRATTACK3:
                break;
            case Player1State.AIRATTACK4:
                break;
            case Player1State.DIVE:
                break;
            case Player1State.GROUNDPOUND:
                break;
            case Player1State.HURT:
                break;
            case Player1State.AIRHURT:
                break;
            case Player1State.CANPICKUP:
                break;
            case Player1State.CANIDLE:
                break;
            case Player1State.CANWALK:
                break;
            case Player1State.CANJUMPUP:
                break;
            case Player1State.CANJUMPMAX:
                break;
            case Player1State.CANJUMPDOWN:
                break;
            case Player1State.CANJUMPLAND:
                break;
            case Player1State.CANSPRINT:
                break;
            case Player1State.DEATH:
                break;
            case Player1State.VICTORY:
                break;
            case Player1State.TAUNT:
                break;
            case Player1State.THROW:
                break;
            case Player1State.SPECIAL:
                break;
            case Player1State.CHARACTERSELECT:
                break;
            default:
                break;
        }
    }

    void OnStateUpdate()
    {
        switch (currentState)
        {
            case Player1State.IDLE:

                //To Walk
                if (dirInput.magnitude != 0) //dirInput != Vector2.zero
                {
                    TransitionToState(Player1State.WALK);
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToState(Player1State.JUMPUP);
                }

                break;
            case Player1State.WALK:

                if (dirInput.magnitude == 0) //dirInput = Vector2.zero
                {
                    TransitionToState(Player1State.IDLE);
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    TransitionToState(Player1State.SPRINT);
                }

                break;
            case Player1State.ATTACK1:
                break;
            case Player1State.ATTACK2:
                break;
            case Player1State.ATTACK3:
                break;
            case Player1State.ATTACK4:
                break;
            case Player1State.SPRINT:

                if(!Input.GetKey(KeyCode.LeftShift))
                {
                    TransitionToState(Player1State.WALK);
                }

                break;
            case Player1State.JUMPUP:
                break;
            case Player1State.JUMPMAX:
                break;
            case Player1State.JUMPDOWN:
                break;
            case Player1State.JUMPLAND:
                break;
            case Player1State.AIRATTACK1:
                break;
            case Player1State.AIRATTACK2:
                break;
            case Player1State.AIRATTACK3:
                break;
            case Player1State.AIRATTACK4:
                break;
            case Player1State.DIVE:
                break;
            case Player1State.GROUNDPOUND:
                break;
            case Player1State.HURT:
                break;
            case Player1State.AIRHURT:
                break;
            case Player1State.CANPICKUP:
                break;
            case Player1State.CANIDLE:
                break;
            case Player1State.CANWALK:
                break;
            case Player1State.CANJUMPUP:
                break;
            case Player1State.CANJUMPMAX:
                break;
            case Player1State.CANJUMPDOWN:
                break;
            case Player1State.CANJUMPLAND:
                break;
            case Player1State.CANSPRINT:
                break;
            case Player1State.DEATH:
                break;
            case Player1State.VICTORY:
                break;
            case Player1State.TAUNT:
                break;
            case Player1State.THROW:
                break;
            case Player1State.SPECIAL:
                break;
            case Player1State.CHARACTERSELECT:
                break;
            default:
                break;
        }
    }

    void OnStateFixedUpdate()
    {
        switch (currentState)
        {
            case Player1State.IDLE:

                break;
            case Player1State.WALK:

                rb2D.velocity = dirInput.normalized * speed;

                break;
            case Player1State.ATTACK1:
                break;
            case Player1State.ATTACK2:
                break;
            case Player1State.ATTACK3:
                break;
            case Player1State.ATTACK4:
                break;
            case Player1State.SPRINT:

                rb2D.velocity = dirInput.normalized * sprintSpeed;

                break;
            case Player1State.JUMPUP:

                //On code le saut 
                if (jumpTimer < jumpDuration)
                {
                    jumpTimer += Time.deltaTime;

                    //Progression / maximum = %
                    float y = jumpCurve.Evaluate(jumpTimer / jumpDuration);

                    graphics.localPosition = new Vector3(transform.localPosition.x, y * jumpHeight, transform.localPosition.z);
                }

                StartCoroutine(GoIdle());

                break;
            case Player1State.JUMPMAX:
                break;
            case Player1State.JUMPDOWN:
                break;
            case Player1State.JUMPLAND:
                break;
            case Player1State.AIRATTACK1:
                break;
            case Player1State.AIRATTACK2:
                break;
            case Player1State.AIRATTACK3:
                break;
            case Player1State.AIRATTACK4:
                break;
            case Player1State.DIVE:
                break;
            case Player1State.GROUNDPOUND:
                break;
            case Player1State.HURT:
                break;
            case Player1State.AIRHURT:
                break;
            case Player1State.CANPICKUP:
                break;
            case Player1State.CANIDLE:
                break;
            case Player1State.CANWALK:
                break;
            case Player1State.CANJUMPUP:
                break;
            case Player1State.CANJUMPMAX:
                break;
            case Player1State.CANJUMPDOWN:
                break;
            case Player1State.CANJUMPLAND:
                break;
            case Player1State.CANSPRINT:
                break;
            case Player1State.DEATH:
                break;
            case Player1State.VICTORY:
                break;
            case Player1State.TAUNT:
                break;
            case Player1State.THROW:
                break;
            case Player1State.SPECIAL:
                break;
            case Player1State.CHARACTERSELECT:
                break;
            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case Player1State.IDLE:
                break;
            case Player1State.WALK:

                animator.SetBool("WALK", false);

                break;
            case Player1State.ATTACK1:
                break;
            case Player1State.ATTACK2:
                break;
            case Player1State.ATTACK3:
                break;
            case Player1State.ATTACK4:
                break;
            case Player1State.SPRINT:

                animator.SetBool("SPRINT", false);

                break;
            case Player1State.JUMPUP:
                break;
            case Player1State.JUMPMAX:
                break;
            case Player1State.JUMPDOWN:
                break;
            case Player1State.JUMPLAND:
                break;
            case Player1State.AIRATTACK1:
                break;
            case Player1State.AIRATTACK2:
                break;
            case Player1State.AIRATTACK3:
                break;
            case Player1State.AIRATTACK4:
                break;
            case Player1State.DIVE:
                break;
            case Player1State.GROUNDPOUND:
                break;
            case Player1State.HURT:
                break;
            case Player1State.AIRHURT:
                break;
            case Player1State.CANPICKUP:
                break;
            case Player1State.CANIDLE:
                break;
            case Player1State.CANWALK:
                break;
            case Player1State.CANJUMPUP:
                break;
            case Player1State.CANJUMPMAX:
                break;
            case Player1State.CANJUMPDOWN:
                break;
            case Player1State.CANJUMPLAND:
                break;
            case Player1State.CANSPRINT:
                break;
            case Player1State.DEATH:
                break;
            case Player1State.VICTORY:
                break;
            case Player1State.TAUNT:
                break;
            case Player1State.THROW:
                break;
            case Player1State.SPECIAL:
                break;
            case Player1State.CHARACTERSELECT:
                break;
            default:
                break;
        }
    }

    void TransitionToState(Player1State nextState)
    {
        OnStateExit();

        currentState = nextState;

        OnStateEnter();
    }

    IEnumerator GoIdle()
    {
        yield return new WaitForSeconds(2f);

        TransitionToState(Player1State.IDLE);
    }

}
