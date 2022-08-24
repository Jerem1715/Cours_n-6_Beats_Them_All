using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] GameObject winUI;
    [SerializeField] GameObject looseUI;


    private int score;
    //private int life;

    private void Awake()// actif au préchargement de la scène
    {
        if (instance == null) instance = this;
        Time.timeScale = 1; //
    }

    #region Condition de victoire

    //le start c'est qd l'objet est présent au début de la scène
    public void Win()
    {
        winUI.SetActive(true);
        Debug.Log("YOU WIN");
        Pause();

    }
    #endregion


    #region Condition de défaite
    public void Loose()
    {
        looseUI.SetActive(true);
        Pause();
        Debug.Log("YOU LOOSE");
    }
    #endregion

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    //public void AddScore(int amount) // amount => montant 
    //{
    //    score += amount;

    //    scoreUI.text = "score: " + score.ToString("d5");
    //    //lz même opération
    //    //scoreUI.text = $"score: {score.ToString("d5")}";
    //}


}
