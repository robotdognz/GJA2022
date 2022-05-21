using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    AudioSource player;
    [SerializeField] AudioClip[] outOfWater;

    private void Start() {
        player = GetComponent<AudioSource>();
    }

    public void OutOfWater()
    {
         player.clip = outOfWater[Random.Range(0, outOfWater.Length)];
         player.Play ();
    }
}
