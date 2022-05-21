using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    // public static bool gamePaused = false;

    public GameObject pauseMenuUI;

    [SerializeField] TextMeshProUGUI deathText;

    // void Update()
    // {
    //     if(gamePaused)
    //     {

    //     }


    // }

    public void TryAgain()
    {
        // Debug.Log("Try Again");
        // gamePaused = false;
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
        // FindObjectOfType<GameManager>().Restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        // gamePaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GameOverWater()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        deathText.text = "Congratulations! You finished transitioning into a water creature";
    }

    public void GameOverLand()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        deathText.text = "Congratulations! You finished transitioning into a land creature";
    }
}

