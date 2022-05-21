using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    AudioSource player;
    [SerializeField] AudioClip[] outOfWater;
    [SerializeField] AudioClip[] intoWater;

    private void Start() {
        player = GetComponent<AudioSource>();
    }

    public void OutOfWater()
    {
         player.clip = outOfWater[Random.Range(0, outOfWater.Length)];
         player.Play ();
    }

    public void IntoWater()
    {
         player.clip = intoWater[Random.Range(0, intoWater.Length)];
         player.Play ();
    }

    
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
            OutOfWater();
        }
    }
}
