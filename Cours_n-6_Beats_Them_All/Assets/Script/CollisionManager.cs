using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //d�g�t de l'enemy
        if (collision.gameObject.tag=="Enemy")
        {
            Debug.Log("enemy touch�");
            collision.gameObject.GetComponent<Enemy2SM>().TakeDamage(damage);
        }

        //d�g�t du player
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player touch�");
            collision.gameObject.GetComponent<PlayerSM>().TakeDamage(damage);
        }


    }
}
