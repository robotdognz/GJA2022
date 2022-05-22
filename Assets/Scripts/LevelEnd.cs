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

            GameManager manager = FindObjectOfType<GameManager>();

            StartCoroutine(manager.WinGame());
        }
    }
}
