using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] DataScriptable healthData;

    [SerializeField] TextMeshProUGUI lifeUI;

    int health;

    bool isDead;



    // Start is called before the first frame update
    void Start()
    {
        health = healthData.startHealth;


        if (gameObject.tag == "Player")
        {
            lifeUI.text = $"Life : {health}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
