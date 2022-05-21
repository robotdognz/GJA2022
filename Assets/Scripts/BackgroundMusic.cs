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

    public IEnumerator StartFadeOut(float duration)
    {
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
