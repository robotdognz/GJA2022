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

    AudioManager audioManager;

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

        // make sure time is running
        Time.timeScale = 1;

        // get audio manager
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        if (audioManagers != null && audioManagers.Length > 1)
        {
            foreach (AudioManager manager in audioManagers)
            {
                if (!manager.isActiveAndEnabled)
                {
                    continue;
                }

                audioManager = manager;
            }
        }
        else if (audioManagers.Length == 1)
        {
            audioManager = audioManagers[0];
        }
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
            }
            else
            {
                transitionBar.gameObject.transform.Find("Background").GetComponent<Image>().color = fine;
            }
        }
    }


    private void timerEnded()
    {
        // end game
        RestartGame();
    }

    public void RestartGame()
    {
        audioManager.PlayGameOverClip();
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
        // pause for a moment before ending
        float fadeOutTime = 1;
        yield return new WaitForSecondsRealtime(fadeOutTime);

        // load end game scene
        audioManager.StopAmbiance();
        SceneManager.LoadScene("99_EndGame");
    }

    void StartTimer()
    {
        timerRunning = true;
        timer = endGameAfterFullTransitionTime;

        audioManager.PlayWarningClip();
    }

    void StopTimer()
    {
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
