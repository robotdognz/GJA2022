using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // load end game scene, last scene in build
            // SceneManager.LoadScene(SceneManager.sceneCount - 1);

            // SceneManager.LoadScene("99_EndGame");

            StartCoroutine(WinGame());
        }
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("99_EndGame");
    }
}
