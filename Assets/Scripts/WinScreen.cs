using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    
    AudioManager audioManager;

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
            audioManager.StartMenuMusic();
        }
    }

    public void PointerEnter()
    {
        audioManager.PlayUIHoverClip();
    }
    
    public void GoToMainMenu()
    {
        audioManager.PlayUIClickClip();
        SceneManager.LoadScene(0);
    }
}
