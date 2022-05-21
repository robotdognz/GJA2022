using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    [SerializeField] AudioSource music;

    void Start()
    {
        if (!music.isPlaying)
        {
            music.Play();
        }
    }

    void Awake()
    {

        int numScenePersists = FindObjectsOfType<BackgroundMusic>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
