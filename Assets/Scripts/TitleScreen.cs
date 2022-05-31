using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    AudioSource music;

    private void Awake() {
        Time.timeScale = 1;

        music = GetComponent<AudioSource>();
    }

    private void Start()
    {
        BackgroundMusic mainGameMusic = FindObjectOfType<BackgroundMusic>();
        if (mainGameMusic != null)
        {
            StartCoroutine(mainGameMusic.StartFadeOut(0.2f));
        }

        music.Play();
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        // load next level
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(NewGame());
    }

    IEnumerator NewGame()
    {
        float fadeTime = 0.2f;
        StartCoroutine(StartFadeOut(fadeTime));
        yield return new WaitForSecondsRealtime(fadeTime);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public IEnumerator StartFadeOut(float duration)
    {
        Debug.Log("Fade Music");
        float currentTime = 0;
        float start = music.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            music.volume = Mathf.Lerp(start, 0, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
