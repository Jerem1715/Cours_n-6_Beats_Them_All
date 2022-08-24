using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //dégât de l'enemy
        if (collision.gameObject.tag=="Enemy")
        {
            Debug.Log("enemy touché");
            collision.gameObject.GetComponent<Enemy2SM>().TakeDamage(damage);
        }

        //dégât du player
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player touché");
            collision.gameObject.GetComponent<PlayerSM>().TakeDamage(damage);
        }


    }
}
