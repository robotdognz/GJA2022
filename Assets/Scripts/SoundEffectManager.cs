using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    AudioSource defaultSource;

    [Header("Player")]
    [Header("Jump")]
    [SerializeField] AudioClip[] jump;
    [SerializeField] AudioSource jumpSource;

    [Header("Out of water")]
    [SerializeField] AudioClip[] outOfWater;
    [SerializeField] AudioSource outOfWaterSource;

    [Header("Into water")]
    [SerializeField] AudioClip[] intoWater;
    [SerializeField] AudioSource intoWaterSource;

    [Header("Other")]
    [Header("Normal Ambiance")]
    [SerializeField] AudioClip normalAmbiance;
    [SerializeField] AudioSource normalAmbianceSource;

    [Header("Water Ambiance")]
    [SerializeField] AudioClip waterAmbiance;
    [SerializeField] AudioSource waterAmbianceSource;

    [Header("UI")]

    [Header("TransitionWarning")]
    [SerializeField] AudioClip warning;
    [SerializeField] AudioSource warningSource;

    [Header("Game Over")]
    [SerializeField] AudioClip gameOver;

    private void Start()
    {
        defaultSource = GetComponent<AudioSource>();
    }

    public void Jump()
    {
        if (jump.Length == 0)
        {
            return;
        }

        jumpSource.clip = jump[Random.Range(0, jump.Length)];
        jumpSource.Play();
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

    public void Warning()
    {
        warningSource.clip = warning;
        warningSource.Play();
    }

    public void StartWaterAmbiance()
    {
        waterAmbianceSource.clip = waterAmbiance;
        waterAmbianceSource.Play();
    }

    public void StopWaterAmbiance()
    {
        waterAmbianceSource.Stop();
    }

    public void StartNormalAmbiance()
    {
        normalAmbianceSource.clip = normalAmbiance;
        normalAmbianceSource.Play();
    }

    public void StopNormalAmbiance()
    {
        normalAmbianceSource.Stop();
    }

    // public void GameOver()
    // {
    //     defaultSource.clip = gameOver;
    //     defaultSource.Play();
    // }
}
