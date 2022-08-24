using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] PlayerSM playerSM;
    private int hp;
   

    private Image healthbar;

    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponent<Image>();
    }


    // Actualise les points de vie pour rester entre 0 et hpmax
    public void UpdateHealth()
    {

        hp = Mathf.Clamp(playerSM.health, 0, playerSM.healthMax);
        float amount = (float)playerSM.health / playerSM.healthMax;
        healthbar.fillAmount = amount;
    }
}
