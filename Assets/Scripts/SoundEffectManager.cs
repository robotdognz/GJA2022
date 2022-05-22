using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    AudioSource defaultSource;
    [Header("Out of water")]
    [SerializeField] AudioClip[] outOfWater;
    [SerializeField] AudioSource outOfWaterSource;

    [Header("Into water")]
    [SerializeField] AudioClip[] intoWater;
    [SerializeField] AudioSource intoWaterSource;


    [Header("Game Over")]
    [SerializeField] AudioClip gameOver;

    private void Start()
    {
        defaultSource = GetComponent<AudioSource>();
    }

    public void OutOfWater()
    {
        if (outOfWater.Length == 0)
        {
            return;
        }

        outOfWaterSource.clip = outOfWater[Random.Range(0, outOfWater.Length)];
        outOfWaterSource.Play();
    }

    public void IntoWater()
    {
        if (intoWater.Length == 0)
        {
            return;
        }

        intoWaterSource.clip = intoWater[Random.Range(0, intoWater.Length)];
        intoWaterSource.Play();
    }

    public void GameOver()
    {
        defaultSource.clip = gameOver;
        defaultSource.Play();
    }
}
