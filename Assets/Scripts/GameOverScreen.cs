using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject HUD_UI;
    [SerializeField] TextMeshProUGUI deathText;

    AudioManager audioManager;

    private void Start() {
        // get audio manager
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        if (audioManagers != null && audioManagers.Length > 1)
        {
            foreach (AudioManager manager in audioManagers)
            {
                if (!manager.isActiveAndEnabled)
                {
                    continue;
                }

                audioManager = manager;
            }
        }
        else if (audioManagers.Length == 1)
        {
            audioManager = audioManagers[0];
        }
    }


    public void TryAgain()
    {
        Time.timeScale = 1f;
        audioManager.PlayUIClickClip();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        audioManager.PlayUIClickClip();
        SceneManager.LoadScene(0);
    }

    public void GameOverWater()
    {
        pauseMenuUI.SetActive(true);
        HUD_UI.SetActive(false);
        Time.timeScale = 0;
        deathText.text = "Congratulations! You became a water creature!";
    }

    public void GameOverLand()
    {
        pauseMenuUI.SetActive(true);
        HUD_UI.SetActive(false);
        Time.timeScale = 0;
        deathText.text = "Congratulations! You became a land creature!";
    }

    public void PointerEnter()
    {
        audioManager.PlayUIHoverClip();
    }
}

