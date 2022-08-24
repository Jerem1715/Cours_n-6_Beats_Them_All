using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevel(int levelindex)
    {
        SceneManager.LoadScene(levelindex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
