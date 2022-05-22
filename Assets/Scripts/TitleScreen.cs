using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    private void Awake() {
        Time.timeScale = 1;
    }

    private void Start()
    {
        BackgroundMusic mainGameMusic = FindObjectOfType<BackgroundMusic>();
        if (mainGameMusic != null)
        {
            StartCoroutine(mainGameMusic.StartFadeOut(1));
        }
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        // load next level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
