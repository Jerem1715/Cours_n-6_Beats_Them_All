using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] AnimationCurve jumpCurve;

    [SerializeField] float jumpHeight = 3f;

    [SerializeField] float jumpDuration = 3f;

    Transform graphics;

    float jumpTimer;


    private void Awake()
    {
        graphics = transform.Find("GraphicsP1");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpTimer < jumpDuration)
        {
            jumpTimer += Time.deltaTime;

            //Progression / maximum = %
            float y = jumpCurve.Evaluate(jumpTimer / jumpDuration);

            graphics.localPosition = new Vector3(transform.localPosition.x, y * jumpHeight, transform.localPosition.z);
        }
        else
        {
            jumpTimer = 0f;
        }
    }
}
