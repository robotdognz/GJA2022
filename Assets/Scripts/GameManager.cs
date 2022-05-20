using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Slider transitionBar;
    [SerializeField] float transitionSpeed = 0.1f;
    [SerializeField] float endGameAfterFullTransitionTime = 2;

    // transition
    float transition = 0;
    float transitionMin = 0; // full water
    float transitionMax = 1; // full land

    // end game timer
    float timer;
    bool timerRunning = false;

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
        }

    }


    private void timerEnded()
    {
        // end game
        Debug.Log("Game Over");
    }

    void StartTimer()
    {
        Debug.Log("Timer started");
        timerRunning = true;
    }


    public void IncrementWater()
    {
        if (!timerRunning)
        {
            transition = Mathf.Clamp(transition - transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
            if (transition == transitionMin)
            {
                StartTimer();
            }
        }
    }

    public void IncrementLand()
    {
        if (!timerRunning)
        {
            transition = Mathf.Clamp(transition + transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
            if (transition == transitionMax)
            {
                StartTimer();
            }
        }
    }

    public float GetTransitionLevel()
    {
        return transition;
    }
}
