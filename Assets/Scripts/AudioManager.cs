using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Music Mixer")]
    [SerializeField][Range(0f, 1f)] float menuMusicVolume = 1f;
    [SerializeField][Range(0f, 1f)] float gameMusicVolume = 1f;

    [Header("Player Mixer")]
    [SerializeField][Range(0f, 1f)] float jumpVolume = 1f;
    [SerializeField][Range(0f, 1f)] float footstepsVolume = 1f;
    [SerializeField][Range(0f, 1f)] float intoWaterVolume = 1f;
    [SerializeField][Range(0f, 1f)] float outOfWaterVolume = 1f;

    [Header("Ambiance Mixer")]
    [SerializeField][Range(0f, 1f)] float normalAmbianceVolume = 1f;
    [SerializeField][Range(0f, 1f)] float waterAmbianceVolume = 1f;

    [Header("UI Mixer")]
    [SerializeField][Range(0f, 1f)] float warningVolume = 1f;
    [SerializeField][Range(0f, 1f)] float gameOverVolume = 1f;
    [SerializeField][Range(0f, 1f)] float UIClickVolume = 1f;
    [SerializeField][Range(0f, 1f)] float UIHoverVolume = 1f;

    [Header("Music Clips")]
    [SerializeField] AudioClip menuMusicClip;
    [SerializeField] AudioClip gameMusicClip;

    [Header("Player Clips")]
    [SerializeField] AudioClip[] jumpClips;
    [SerializeField] AudioClip[] intoWaterClips;
    [SerializeField] AudioClip[] outOfWaterClips;
    [SerializeField] AudioClip footstepsClip;

    [Header("Ambiance Clips")]
    [SerializeField] AudioClip normalAmbianceClip;
    [SerializeField] AudioClip waterAmbianceClip;

    [Header("UI Clips")]
    [SerializeField] AudioClip[] warningClips;
    [SerializeField] AudioClip[] gameOverClips;
    [SerializeField] AudioClip[] UIClickClips;
    [SerializeField] AudioClip[] UIHoverClips;

    [Header("Sources")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource ambiance;
    [SerializeField] AudioSource jumpSource;
    [SerializeField] AudioSource footsteps;
    [SerializeField] AudioSource outOfWaterSource;
    [SerializeField] AudioSource intoWaterSource;
    [SerializeField] AudioSource warningSource;
    [SerializeField] AudioSource gameOverSource;
    [SerializeField] AudioSource UISource;

    // --------- Music -----------------

    // TODO


    // --------- Player -----------------

    public void PlayJumpClip()
    {
        PlayClipWithSource(jumpClips, jumpVolume, jumpSource);
    }

    public void PlayIntoWaterClip()
    {
        PlayClipWithSource(intoWaterClips, intoWaterVolume, intoWaterSource);
    }

    public void PlayOutOfWaterClip()
    {
        PlayClipWithSource(outOfWaterClips, outOfWaterVolume, outOfWaterSource);
    }

    public void StartFootsteps()
    {
        footsteps.clip = footstepsClip;
        footsteps.volume = footstepsVolume;
        footsteps.time = Random.value * footstepsClip.length;
        footsteps.Play();
    }

    public void StopFootsteps()
    {
        footsteps.Stop();
    }


    // ---------- Ambiance ----------------

    private void Start()
    {
        StartNormalAmbiance();
    }
    public void StartNormalAmbiance()
    {
        ambiance.clip = normalAmbianceClip;
        ambiance.volume = normalAmbianceVolume;
        ambiance.time = Random.value * normalAmbianceClip.length;
        ambiance.Play();
    }

    public void StartWaterAmbiance()
    {
        ambiance.clip = waterAmbianceClip;
        ambiance.volume = waterAmbianceVolume;
        ambiance.time = Random.value * waterAmbianceClip.length;
        ambiance.Play();
    }


    // --------- UI -----------------

    public void PlayWarningClip()
    {
        PlayClipWithSource(warningClips, warningVolume, warningSource);
    }

    public void PlayGameOverClip()
    {
        PlayClipWithSource(gameOverClips, gameOverVolume, gameOverSource);
    }

    public void PlayUIClickClip()
    {
        PlayClipWithSource(UIClickClips, UIClickVolume, UISource);
    }

    public void PlayUIHoverClip()
    {
        PlayClipWithSource(UIHoverClips, UIHoverVolume, UISource);
    }

    // ----------- Helper ----------------

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

}
