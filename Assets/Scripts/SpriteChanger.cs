using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    SpriteRenderer sRenderer;
    [SerializeField] Sprite[] sprites;

    Animator animator;
    // float previousSpeed = 1;

    private void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void PauseAnimation()
    {
        // previousSpeed = animator.speed;
        animator.speed = 0;
    }

    public void PlayAnimation(float speed)
    {
        animator.speed = speed; //previousSpeed;
    }

    public void UpdateSprite(float transition)
    {
        if (transition >= 0.75f)
        {
            sRenderer.sprite = sprites[3];
        }
        else if (transition >= 0.5f)
        {
            sRenderer.sprite = sprites[2];
        }
        else if (transition >= 0.25f)
        {
            sRenderer.sprite = sprites[1];
        }
        else
        {
            sRenderer.sprite = sprites[0];
        }
    }
}
