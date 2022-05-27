using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
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

        if (audioManager != null)
        {
            audioManager.StopAmbiance();
            if (!audioManager.IsPlayingMenuMusic())
            {
                audioManager.StartMenuMusic();
            }
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");

        // switch sounds
        if (audioManager != null)
        {
            audioManager.StartGameMusic();
            audioManager.StartNormalAmbiance();
            audioManager.PlayUIClickClip();
        }

        // load next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        audioManager.PlayUIClickClip();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void PointerEnter()
    {
        audioManager.PlayUIHoverClip();
    }
}
