using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float landRunSpeed = 5;
    [SerializeField] float landJumpPower = 10;
    [SerializeField] float landGravity = 5;

    [SerializeField] float waterRunSpeed = 5;
    [SerializeField] float waterJumpPower = 10;
    [SerializeField] float waterGravity = 5;

    bool isInWater = false;
    float runSpeed;
    float jumpPower;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;

    // game management
    GameManager manager;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();

        manager = FindObjectOfType<GameManager>();

        // myRigidBody.gravityScale = landGravity;
        // runSpeed = landRunSpeed;
        jumpPower = landJumpPower;
        // isInWater = false;
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

    void Update()
    {
        UpdateTerranType();
        Run();
    }

    private void UpdateTerranType()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            // runSpeed = landRunSpeed;
            // jumpPower = landJumpPower;
            // myRigidBody.gravityScale = landGravity;
            isInWater = false;
            manager.IncrementLand();
        }
        else
        {
            // runSpeed = waterRunSpeed;
            // jumpPower = waterJumpPower;
            // myRigidBody.gravityScale = waterGravity;
            isInWater = true;
            manager.IncrementWater();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {

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
