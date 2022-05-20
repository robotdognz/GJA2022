using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Slider transitionBar;
    [SerializeField] float transitionSpeed = 0.1f;

    float transition = 0;
    float transitionMin = 0;
    float transitionMax = 1;

    // Start is called before the first frame update
    void Start()
    {
        transitionBar.value = transition;
        transitionBar.maxValue = transitionMax;
    }

    // Update is called once per frame
    void Update()
    {
        // if (transition < transitionMax)
        // {
        //     transition += 0.001f;
        // }
        transitionBar.value = transition;
    }

    public void IncrementWater()
    {
        transition = Mathf.Clamp(transition + transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
    }

    public void IncrementLand()
    {
        transition = Mathf.Clamp(transition - transitionSpeed * Time.deltaTime, transitionMin, transitionMax);
    }
}
