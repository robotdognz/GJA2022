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

    // transition
    float transition = 0;
    float transitionMin = 0; // full water
    float transitionMax = 1; // full land
    bool gameOver = false;

    // end game timer
    float timer;
    bool timerRunning = false;

    // transition bar
    Color fine = new Color(1, 1, 1);
    Color bad = new Color(1, 0, 0);

    void Start()
    {
        // setup transition UI element
        transitionBar.value = transition;
        transitionBar.minValue = transitionMin;
        transitionBar.maxValue = transitionMax;

        // setup timer
        timer = endGameAfterFullTransitionTime;
    }

    void Update()
    {
        if (!gameOver)
        {
            // transition
            transitionBar.value = transition;

            // timer
            if (timerRunning)
            {
                timer -= Time.deltaTime;

                if (timer <= 0.0f)
                {
                    timerEnded();
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
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void StartTimer()
    {
        Debug.Log("Timer started");
        timerRunning = true;
        timer = endGameAfterFullTransitionTime;
    }

    void StopTimer()
    {
        Debug.Log("Timer stopped");
        timerRunning = false;
    }


    public void IncrementWater()
    {
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
}
