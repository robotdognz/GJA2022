using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    SpriteRenderer sRenderer;
    [SerializeField] Sprite[] sprites;

    private void Start() {
        sRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void UpdateSprite(float transition)
    {
        if (transition >= 0.75f)
        {
            sRenderer.sprite = sprites[3];
        }
        else if(transition >= 0.5f)
        {
            sRenderer.sprite = sprites[2];
        }
        else if(transition >= 0.25f)
        {
            sRenderer.sprite = sprites[1];
        }
        else
        {
            sRenderer.sprite = sprites[0];
        }
    }
}
