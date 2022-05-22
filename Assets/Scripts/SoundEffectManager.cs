using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    AudioSource player;
    [SerializeField] AudioClip[] outOfWater;
    [SerializeField] AudioClip[] intoWater;
    [SerializeField] AudioClip gameOver;

    private void Start()
    {
        player = GetComponent<AudioSource>();
    }

    public void OutOfWater()
    {
        if (outOfWater.Length == 0)
        {
            return;
        }

        player.clip = outOfWater[Random.Range(0, outOfWater.Length)];
        player.Play();
    }

    public void IntoWater()
    {
        if (intoWater.Length == 0)
        {
            return;
        }

        player.clip = intoWater[Random.Range(0, intoWater.Length)];
        player.Play();
    }

    public void GameOver()
    {
        player.clip = gameOver;
        player.Play();
    }
}
