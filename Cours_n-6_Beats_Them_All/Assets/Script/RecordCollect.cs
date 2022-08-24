using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordCollect : MonoBehaviour
{

    [SerializeField] int score;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            //Une fois collectée par le player
            Destroy(gameObject);

            GameManager.instance.AddScore(score);

        }
    }
}
