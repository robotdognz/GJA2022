using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Full Land Creature")]
    [SerializeField] float landRunSpeed = 5;
    [SerializeField] float landJumpPower = 10;
    [SerializeField] float landGravity = 5;

    [Header("Full Water Creature")]
    [SerializeField] float waterRunSpeed = 5;
    [SerializeField] float waterJumpPower = 10;
    [SerializeField] float waterGravity = 5;

    bool isInWater = false;
    float runSpeed = 5;
    float jumpPower = 10;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;

    // game management
    GameManager gameManager;

    void Start()
    {
        // setup fields
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        UpdateTerranType();
        Run();
        if (gameManager.IsGameOver())
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }

    private void UpdateTerranType()
    {
        float currentTransition = gameManager.GetTransitionLevel();

        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            // runSpeed = landRunSpeed;
            // jumpPower = landJumpPower;
            // myRigidBody.gravityScale = landGravity;
            isInWater = false;
            gameManager.IncrementLand();
        }
        else
        {
            // runSpeed = waterRunSpeed;
            // jumpPower = waterJumpPower;
            // myRigidBody.gravityScale = waterGravity;
            isInWater = true;
            gameManager.IncrementWater();
        }
    }

    Vector2 Lerp(Vector2 start, Vector2 end, float percent)
    {
        return (start + percent * (end - start));
    }

    private void Run()
    {
        if (!isInWater)
        {
            // do land movement
            Vector2 playerVelocity = new Vector2(moveInput.x * landRunSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
        }
        else
        {
            // do water movement
            float yMovement = Mathf.Clamp(myRigidBody.velocity.y + moveInput.y * waterRunSpeed, -10, 10);
            Vector2 playerVelocity = new Vector2(moveInput.x * waterRunSpeed, yMovement);
            myRigidBody.velocity = playerVelocity;
        }
    }

    // ----------- process input actions -------------

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // only jump when touching platforms or in water
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Platforms", "Water")))
        {
            return;
        }

        if (value.isPressed)
        {
            Debug.Log("Jump");
            myRigidBody.velocity += new Vector2(0, jumpPower);
        }
    }
}
