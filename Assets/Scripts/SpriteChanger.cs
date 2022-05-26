using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    // SpriteRenderer sRenderer;
    // [SerializeField] Sprite[] sprites;

    Animator animator;

    private void Start()
    {
        // sRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void PauseAnimation()
    {
        animator.speed = 0;
    }

    public void PlayAnimation(float speed)
    {
        animator.speed = speed; 
    }

    public void UpdateSprite(float transition)
    {

        animator.SetFloat("Transition", transition);

    }
}
