using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] bool waterFood = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            if(!waterFood)
            {
            FindObjectOfType<GameManager>().IncrementLand(0.25f);
            }
            else
            {
                FindObjectOfType<GameManager>().IncrementWater(0.25f);
            }
            Destroy(gameObject);
        }
    }
}
