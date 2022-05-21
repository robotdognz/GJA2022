using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Full Land Creature")]
    [SerializeField] float landCreature_RunSpeed = 10;
    [SerializeField] Vector2 landCreature_SwimSpeed = new Vector2(0, 5);
    [SerializeField] float landCreature_landJumpPower = 10;
    [SerializeField] float landCreature_waterJumpPower = 0;
    [SerializeField] float landCreatureBuoyancy = 2;

    [Header("Full Water Creature")]
    [SerializeField] float waterCreature_RunSpeed = 3;
    [SerializeField] Vector2 waterCreature_SwimSpeed = new Vector2(7, 7);
    [SerializeField] float waterCreature_landJumpPower = 0.5f;
    [SerializeField] float waterCreature_waterJumpPower = 20;
    [SerializeField] float waterCreatureBuoyancy = 0;

    [Header("General Land Movement")]
    [SerializeField] float movementIncrease = 0.2f;
    [SerializeField] float movementSlow = 0.98f;

    // current state
    bool isInWater = false;
    bool onGround = false;
    float currentRunSpeed;
    Vector2 currentSwimSpeed;
    float currentLandJumpPower;
    float currentWaterJumpPower;
    float currentBuoyancy;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Collider2D myCollider;
    BoxCollider2D myFeetCollider;
    // BuoyancyEffector2D water;
    BuoyancyEffector2D[] waters;

    // game management
    GameManager gameManager;

    void Start()
    {
        // setup fields
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        waters = FindObjectsOfType<BuoyancyEffector2D>();
    }

    void Update()
    {
        UpdateTerranType();
        UpdateMovementSkill();
        Run();

        // flip player
        bool playerHasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

        if (gameManager.IsGameOver())
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        }
    }

    private void UpdateMovementSkill()
    {
        float transition = gameManager.GetTransitionLevel();

        currentRunSpeed = FloatLerp(waterCreature_RunSpeed, landCreature_RunSpeed, transition);
        currentSwimSpeed = VectorLerp(waterCreature_SwimSpeed, landCreature_SwimSpeed, transition);

        currentLandJumpPower = FloatLerp(waterCreature_landJumpPower, landCreature_landJumpPower, transition);
        currentWaterJumpPower = FloatLerp(waterCreature_waterJumpPower, landCreature_waterJumpPower, transition);

        currentBuoyancy = FloatLerp(waterCreatureBuoyancy, landCreatureBuoyancy, transition);

        foreach (BuoyancyEffector2D water in waters)
        {
            water.density = currentBuoyancy;
        }

        // Debug.Log(currentRunSpeed);
    }

    // transition calculation methods
    float FloatLerp(float start, float end, float percent)
    {
        return (start + percent * (end - start));
    }
    Vector2 VectorLerp(Vector2 start, Vector2 end, float percent)
    {
        return (start + percent * (end - start));
    }

    private void UpdateTerranType()
    {
        float currentTransition = gameManager.GetTransitionLevel();

        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            isInWater = false;
            gameManager.IncrementLand();
        }
        else
        {
            isInWater = true;
            gameManager.IncrementWater();
        }

        onGround = myCollider.IsTouchingLayers(LayerMask.GetMask("Platforms"));

    }

    private void Run()
    {
        float yMovement = myRigidBody.velocity.y;
        float xMovement = myRigidBody.velocity.x;

        if (!isInWater)
        {
            // do land movement


            if (Mathf.Abs(moveInput.x) > 0)
            {
                // moving on the x axis

                if (moveInput.x > 0)
                {
                    // moving right
                    if (myRigidBody.velocity.x < currentRunSpeed)
                    {
                        xMovement = Mathf.Min(myRigidBody.velocity.x + movementIncrease, currentRunSpeed);
                    }
                }
                else
                {
                    // moving left
                    if (myRigidBody.velocity.x > -currentRunSpeed)
                    {
                        xMovement = Mathf.Max(myRigidBody.velocity.x - movementIncrease, -currentRunSpeed);
                    }
                }
            }
            else if (onGround)
            {
                xMovement = myRigidBody.velocity.x * movementSlow;
            }

        }
        else
        {
            // do water movement
            xMovement = myRigidBody.velocity.x + moveInput.x * currentSwimSpeed.x;
            yMovement = myRigidBody.velocity.y + moveInput.y * currentSwimSpeed.y;
        }

        // apply the movement
        Vector2 playerVelocity = new Vector2(xMovement, yMovement);
        myRigidBody.velocity = playerVelocity;
    }

    // ----------- process input actions -------------

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        // only jump when touching platforms or in water
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platforms")) && myCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {
            // do most powerful jump
            float tempJumpPower = Mathf.Max(currentWaterJumpPower, currentLandJumpPower);
            myRigidBody.velocity += new Vector2(0, tempJumpPower);
            Debug.Log("Jump" + tempJumpPower);
        }
        else if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platforms")))
        {
            // do land jump
            myRigidBody.velocity += new Vector2(0, currentLandJumpPower);
            Debug.Log("Jump" + currentLandJumpPower);
        }
        else if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
        {

            // do water jump
            myRigidBody.velocity += new Vector2(0, currentWaterJumpPower);
            Debug.Log("Jump" + currentWaterJumpPower);
        }

    }

    void OnToggleMode(InputValue value)
    {
        gameManager.ToggleMode();
    }
}
