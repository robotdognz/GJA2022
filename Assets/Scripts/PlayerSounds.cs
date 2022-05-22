using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    [SerializeField] SoundEffectManager soundManager;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Enter water sound effect");
            soundManager.IntoWater();
            soundManager.StartWaterAmbiance();
            soundManager.StopNormalAmbiance();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Exit water sound effect");
            soundManager.OutOfWater();
            soundManager.StopWaterAmbiance();
            soundManager.StartNormalAmbiance();
        }
    }

    public void Jump()
    {
        soundManager.Jump();
    }
}
