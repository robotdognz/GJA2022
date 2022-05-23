using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Mixer")]
    [SerializeField][Range(0f, 1f)] float jumpVolume = 1f;
    [SerializeField][Range(0f, 1f)] float footstepsVolume = 1f;
    [SerializeField][Range(0f, 1f)] float intoWaterVolume = 1f;
    [SerializeField][Range(0f, 1f)] float outOfWaterVolume = 1f;
    [SerializeField][Range(0f, 1f)] float warningVolume = 1f;
    [SerializeField][Range(0f, 1f)] float gameOverVolume = 1f;
    [SerializeField][Range(0f, 1f)] float normalAmbianceVolume = 1f;
    [SerializeField][Range(0f, 1f)] float waterAmbianceVolume = 1f;

    [Header("Clips")]
    [SerializeField] AudioClip[] jumpClips;
    [SerializeField] AudioClip footstepsClip;
    [SerializeField] AudioClip[] intoWaterClips;
    [SerializeField] AudioClip[] outOfWaterClips;
    [SerializeField] AudioClip[] warningClips;
    [SerializeField] AudioClip[] gameOverClips;
    [SerializeField] AudioClip normalAmbianceClip;
    [SerializeField] AudioClip waterAmbianceClip;
    
    [Header("Sources")]
    [SerializeField] AudioSource ambiance;
    [SerializeField] AudioSource jumpSource;
    [SerializeField] AudioSource footsteps;
    [SerializeField] AudioSource outOfWaterSource;
    [SerializeField] AudioSource intoWaterSource;
    [SerializeField] AudioSource warningSource;
    [SerializeField] AudioSource gameOverSource;

    // --------- one shots -----------------

    public void PlayJumpClip()
    {
        // PlayClipAtPoint(jumpClips, jumpVolume);
        PlayClipWithSource(jumpClips, jumpVolume, jumpSource);
    }

    public void PlayIntoWaterClip()
    {
        // PlayClipAtPoint(intoWaterClips, intoWaterVolume);
        PlayClipWithSource(intoWaterClips, intoWaterVolume, intoWaterSource);
    }

    public void PlayOutOfWaterClip()
    {
        // PlayClipAtPoint(outOfWaterClips, outOfWaterVolume);
        PlayClipWithSource(outOfWaterClips, outOfWaterVolume, outOfWaterSource);
    }

    public void PlayWarningClip()
    {
        // PlayClipAtPoint(warningClips, warningVolume);
        PlayClipWithSource(warningClips, warningVolume, warningSource);
    }

    public void PlayGameOverClip()
    {
        // PlayClipAtPoint(gameOverClips, gameOverVolume);
        PlayClipWithSource(gameOverClips, gameOverVolume, gameOverSource);
    }

    private void PlayClipAtPoint(AudioClip[] clips, float volume)
    {
        if (clips != null && clips.Length > 0)
        {
            int index = Random.Range(0, clips.Length);
            Vector3 cameraPos = Camera.main.transform.position;

            AudioSource.PlayClipAtPoint(clips[index], cameraPos, volume);
        }
    }

    private void PlayClipWithSource(AudioClip[] clips, float volume, AudioSource source)
    {
        if (clips != null && source != null && clips.Length > 0)
        {
            int index = Random.Range(0, clips.Length);

            source.clip = clips[index];
            source.volume = volume;
            source.Play();
        }
    }

    // ---------- loops ----------------

    private void Start() {
        StartNormalAmbiance();
    }
    public void StartNormalAmbiance()
    {
        ambiance.clip = normalAmbianceClip;
        ambiance.volume = normalAmbianceVolume;
        ambiance.Play();
    }

    public void StartWaterAmbiance()
    {
        ambiance.clip = waterAmbianceClip;
        ambiance.volume = waterAmbianceVolume;
        ambiance.Play();
    }

    public void StartFootsteps()
    {
        footsteps.clip = footstepsClip;
        footsteps.volume = footstepsVolume;
        footsteps.Play();
    }

    public void StopFootsteps()
    {
        footsteps.Stop();
    }

}
