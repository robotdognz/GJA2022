using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public AudioManager audioManager;

    private void Start()
    {
        // get audio manager
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        if (audioManagers != null && audioManagers.Length > 1)
        {
            foreach(AudioManager manager in audioManagers)
            {
                if(!manager.isActiveAndEnabled)
                {
                    continue;
                }

                audioManager = manager;
            }
        }
        else if(audioManagers.Length == 1)
        {
            audioManager = audioManagers[0];
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Enter water sound effect");
            audioManager.PlayIntoWaterClip();
            audioManager.StartWaterAmbiance();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Exit water sound effect");
            audioManager.PlayOutOfWaterClip();
            audioManager.StartNormalAmbiance();
        }
    }

    public void Jump()
    {
        audioManager.PlayJumpClip();
    }
}
