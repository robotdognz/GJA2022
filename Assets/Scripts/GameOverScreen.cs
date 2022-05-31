using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public GameObject pauseMenuUI;
    [SerializeField] TextMeshProUGUI deathText;


    public void TryAgain()
    {
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        // float fadeOutTime = 1;
        // StartCoroutine(FindObjectOfType<BackgroundMusic>().StartFadeOut(fadeOutTime));
        // StartCoroutine(BackToMainMenu());
        SceneManager.LoadScene(0);
    }

    // IEnumerator BackToMainMenu()
    // {
    //     Time.timeScale = 1;
    //     float fadeOutTime = 1;
    //     StartCoroutine(FindObjectOfType<BackgroundMusic>().StartFadeOut(fadeOutTime));

    //     yield return new WaitForSecondsRealtime(fadeOutTime);

    //     SceneManager.LoadScene(0);
    // }

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

