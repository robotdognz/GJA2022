using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] Button primaryButton;
    public bool isActive = true;

    PlayerInput playerInput;
    string currentInput = "";
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world!");

        // setup input
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            // set keyboard and mouse input as default
            playerInput.SwitchCurrentControlScheme("Keyboard&Mouse");
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            playerInput = FindObjectOfType<PlayerInput>();
            if (playerInput != null)
            {
                // set keyboard and mouse input as default
                playerInput.SwitchCurrentControlScheme("Keyboard&Mouse");
                EventSystem.current.SetSelectedGameObject(null);
                // EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
            }
        }

        // get audio manager
        AudioManager[] audioManagers = FindObjectsOfType<AudioManager>();
        if (audioManagers != null && audioManagers.Length > 1)
        {
            foreach (AudioManager manager in audioManagers)
            {
                if (!manager.isActiveAndEnabled)
                {
                    continue;
                }

                audioManager = manager;
            }
        }
        else if (audioManagers.Length == 1)
        {
            audioManager = audioManagers[0];
        }
    }


    void OnLook(InputValue value)
    {
        if (isActive)
        {
            if (currentInput == "Keyboard&Mouse")
            {
                // deselect buttons
                EventSystem.current.SetSelectedGameObject(null);
                Debug.Log("Mouse moved");
            }
        }
    }

    void OnNavigate(InputValue value)
    {
        // Debug.Log("Navigate");

        // select first button if none selected
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
            Debug.Log("Switched to button navigation, nav");
        }

        // // play sound if navigating in actual direction (not 0,0)
        // if (value.Get<Vector2>().Equals(new Vector2(0, 1)) || value.Get<Vector2>().Equals(new Vector2(0, -1)))
        // {
        //     audioManager.PlayUIHoverClip();
        // }
    }

    void OnMove(InputValue value)
    {
        if (isActive)
        {
            Debug.Log("Move");
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
                Debug.Log("Switched to button navigation, move");
            }

            // // play sound if navigating in actual direction (not 0,0)
            // if (!value.Get<Vector2>().Equals(new Vector2()))
            // {
            //     audioManager.PlayUIHoverClip();
            // }
        }
    }

    void OnControlsChanged()
    {
        if (isActive && playerInput != null)
        {
            currentInput = playerInput.currentControlScheme;
            Debug.Log(playerInput.currentControlScheme);
        }

    }
}
