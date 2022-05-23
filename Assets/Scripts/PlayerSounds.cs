using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    AudioManager soundManager;

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

                soundManager = manager;
            }
        }
        else if(audioManagers.Length == 1)
        {
            soundManager = audioManagers[0];
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Enter water sound effect");
            soundManager.PlayIntoWaterClip();
            soundManager.StartWaterAmbiance();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Exit water sound effect");
            soundManager.PlayOutOfWaterClip();
            soundManager.StartNormalAmbiance();
        }
    }

    public void Jump()
    {
        soundManager.PlayJumpClip();
    }
}
