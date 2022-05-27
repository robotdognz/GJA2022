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

    // known issues: 
    // - title screen plays button selection sound when it opens
    // - pressing down when no buttons are selected moves down to the second option immediately, instead of starting on the first option

    // Start is called before the first frame update
    void Start()
    {
        // setup input
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
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

        // hide curser
        Cursor.visible = false;
    }


    void OnMouse(InputValue value)
    {
        if (isActive)
        {
            // deselect buttons
            EventSystem.current.SetSelectedGameObject(null);
            Cursor.visible = true;
        }
    }

    void OnNavigate(InputValue value)
    {
        // Debug.Log("Navigate");

        if (isActive)
        {
            // select first button if none selected
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
                // Debug.Log("Switched to button navigation, nav");
            }
            Cursor.visible = false;
        }
    }

    void OnMove(InputValue value)
    {
        // Debug.Log("Move");

        if (isActive)
        {
            // select first button if none selected
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
                // Debug.Log("Switched to button navigation, move");
            }
        }
    }

    void OnControlsChanged()
    {
        if (isActive && playerInput != null)
        {
            currentInput = playerInput.currentControlScheme;
            if (currentInput != "Keyboard&Mouse")
            {
                EventSystem.current.SetSelectedGameObject(primaryButton.gameObject);
            }
            // Debug.Log(playerInput.currentControlScheme);
        }

    }
}
