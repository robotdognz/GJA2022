using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Slider transitionBar;
    [SerializeField] float transitionSpeed = 0.1f;
    [SerializeField] float endGameAfterFullTransitionTime = 2;

    [SerializeField] Scene deathScene;

    [SerializeField] SoundEffectManager soundManager;

    // transition
    float transition = 0.5f;
    float transitionMin = 0; // full water
    float transitionMax = 1; // full land
    bool gameOver = false;
    SpriteChanger spriteChanger;

    // end game timer
    float timer;
    bool timerRunning = false;

    // transition bar
    Color fine = new Color(1, 1, 1);
    Color bad = new Color(1, 0, 0);

    // debug
    bool barRunning = true;

    void Start()
    {
        // setup transition UI element
        transitionBar.value = transition;
        transitionBar.minValue = transitionMin;
        transitionBar.maxValue = transitionMax;

        // player transition sprite
        spriteChanger = FindObjectOfType<SpriteChanger>();

        // setup timer
        timer = endGameAfterFullTransitionTime;

        Time.timeScale = 1;
    }

    void Update()
    {
        if (!gameOver)
        {
            // transition bar
            transitionBar.value = transition;

            // transition player sprite
            spriteChanger.UpdateSprite(transition);

            // timer
            if (timerRunning)
            {
                timer -= Time.deltaTime;

                if (timer <= 0.0f)
                {
                    timerEnded();
                    timerRunning = false;
                }

                transitionBar.gameObject.transform.Find("Background").GetComponent<Image>().color = bad;
                // transitionBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = bad;
            }
            else
            {
                transitionBar.gameObject.transform.Find("Background").GetComponent<Image>().color = fine;
                // transitionBar.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = fine;
            }
        }
    }


    private void timerEnded()
    {
        // end game
        // Debug.Log("Game Over");
        // gameOver = true;
        // remove transition bar UI element
        // transitionBar.gameObject.SetActive(false);
        RestartGame();
    }

    public void RestartGame()
    {
        FindObjectOfType<SoundEffectManager>().GameOver();
        if (transition < 0.5f)
        {
            // water creature
            FindObjectOfType<GameOverScreen>().GameOverWater();
        }
        else
        {
            // land creature
            FindObjectOfType<GameOverScreen>().GameOverLand();
        }
    }

    public IEnumerator WinGame()
    {
        // fade out the background music before ending
        float fadeOutTime = 1;
        // StartCoroutine(FindObjectOfType<BackgroundMusic>().StartFadeOut(fadeOutTime));
        yield return new WaitForSecondsRealtime(fadeOutTime);

        // kill all the background music game objects
        // BackgroundMusic[] backgroundMusics = FindObjectsOfType<BackgroundMusic>();
        // foreach(BackgroundMusic bm in backgroundMusics)
        // {
        //     bm.GetComponent<AudioSource>().Stop();
        //     Destroy(bm);
        // }

        // load end game scene
        SceneManager.LoadScene("99_EndGame");
    }

    void StartTimer()
    {
        // Debug.Log("Timer started");
        timerRunning = true;
        timer = endGameAfterFullTransitionTime;

        //-----------------------------------------------------------------------------------------------------------
        soundManager.Warning();
    }

    void StopTimer()
    {
        // Debug.Log("Timer stopped");
        timerRunning = false;
    }

    public void IncrementWater()
    {
        if (!barRunning)
        {
            return;
        }

        if (!timerRunning)
        {
            transition = Mathf.Clamp(transition - transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
            if (transition <= transitionMin)
            {
                StartTimer();
            }
        }
        else if (transition >= transitionMax)
        {
            // start incrementing transition again and stop timer
            transition = Mathf.Clamp(transition - transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
            StopTimer();
        }
    }

    public void IncrementLand()
    {
        if (!barRunning)
        {
            return;
        }

        if (!timerRunning)
        {
            transition = Mathf.Clamp(transition + transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
            if (transition >= transitionMax)
            {
                StartTimer();
            }
        }
        else if (transition <= transitionMin)
        {
            // start incrementing transition again and stop timer
            transition = Mathf.Clamp(transition + transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
            StopTimer();
        }
    }

    public float GetTransitionLevel()
    {
        return transition;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }


    // ---------- Legacy and Debug ------------

    public void ToggleMode()
    {
        if (transition != transitionMin)
        {
            transition = transitionMin;
        }
        else
        {
            transition = transitionMax;
        }
    }

    public void ToggleTimer()
    {
        barRunning = !barRunning;
    }

    public void IncrementWater(float amount)
    {
        transition = Mathf.Clamp(transition - amount, transitionMin, transitionMax);
    }

    public void IncrementLand(float amount)
    {
        transition = Mathf.Clamp(transition + amount, transitionMin, transitionMax);
    }
}
