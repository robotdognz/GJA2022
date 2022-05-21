using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Enter water sound effect");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Water")
        {
            Debug.Log("Exit water sound effect");
        }
    }
}
