using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float inAirMovementDelta;


    CharacterController controller;
    protected Vector3 moveDirection;
    protected Vector3 planarMovement;

    [Header("Dashing and Charging")]
    float dashCooldownTimer;
    [SerializeField] float dashCooldownTimerMax;
    [SerializeField] private float dashTimerMax;
    private float dashTimer;
    [SerializeField] private float dashSpeed;
    [SerializeField] private AnimationCurve dashCurve;
    Vector3 dashDirection;
    Vector3 dashVelocity;


    [Header("Jumping and Falling")]
    [SerializeField] protected const float gravity = 13f;
    protected float yVelocity;

    [SerializeField] protected LayerMask groundCheckMask;
    [SerializeField] Transform groundCheckRaycastStart;
    [SerializeField] float groundCheckRadius = 1;
    [SerializeField] private int jumpHeight;
    public bool isGrounded;
    bool isJumping;

    //[Header("Misc Forces")]
    protected Vector3 miscForce;
    float miscForceTimer;
    float airResistance;

    // Initialize is called by the CharacterManager
    public virtual void Initialize()
    {
        controller = GetComponent<CharacterController>();
    }
    public void HandleAllMovement()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        
        GroundedCheck();
        HandleFalling();
        TickCooldownTimers(Time.deltaTime);
        HandleDashing();
        HandleMiscForces();
        controller.Move((yVelocity*transform.up + planarMovement) * Time.deltaTime);
    }


    // Gets information about this frame's movement.  For the player this will read from InputHandler, while for enemies it'll read from their AI
    protected virtual Vector3 ProcessMovementInput(Vector2 moveInput)
    {
        return moveInput;
    }

    public virtual void SetMoveDirection(Vector2 moveInput) 
    {
        if(dashTimer > 0 || miscForceTimer > 0)
        {
            return;
        }
        moveDirection = ProcessMovementInput(moveInput);
        if (isGrounded)
        {
            planarMovement = moveSpeed*moveDirection.normalized;
        }
        else
        {
            Accelerate(moveDirection);
            //planarMovement += inAirMovementDelta * moveDirection * Time.deltaTime;
            //if(planarMovement.sqrMagnitude > moveSpeed*moveSpeed)
            //{
            //    planarMovement = moveSpeed*planarMovement.normalized;
            //}
        }
    }

    private void Accelerate(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            return;
        }
        Vector3 desiredVelocity = moveSpeed * direction.normalized;
        Vector3 acceleration = new Vector3(desiredVelocity.x - planarMovement.x, 0, desiredVelocity.z - planarMovement.z) / Time.deltaTime;
        if (acceleration.sqrMagnitude > inAirMovementDelta * inAirMovementDelta)
        {
            acceleration = acceleration.normalized * inAirMovementDelta;
        }
        planarMovement += acceleration;
    }

    // Decrements dash cooldown.  This is entirely here so that PlayerLocomotion doesn't have to have its own update function or override HandleAllMovement
    protected virtual void TickCooldownTimers(float tickLength)
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= tickLength;
        }
    }

    #region Dashing and Charging
    public void AttemptDash(Vector3 direction)
    {
        if(dashTimer > 0 || dashCooldownTimer > 0 || miscForceTimer > 0)
        {
            return;
        }
        dashTimer = dashTimerMax;
        dashDirection = (direction != null)? (ProcessMovementInput(direction)): moveDirection;
        if (!isGrounded)
        {
            moveDirection = dashDirection;
        }
    }
    private void HandleDashing()
    {
        if(dashTimer > 0)
        {
            planarMovement = dashSpeed * dashCurve.Evaluate(dashTimer / dashTimerMax) * dashDirection;
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                dashVelocity = Vector3.zero;
                dashCooldownTimer = dashCooldownTimerMax;
            }
        }

    }
    #endregion

    #region Jumping and Falling
    private void GroundedCheck()
    {
        if (Physics.CheckSphere(groundCheckRaycastStart.position, groundCheckRadius, groundCheckMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    protected virtual void HandleFalling()
    {
        if (!isGrounded)
        {
            yVelocity -= gravity*Time.deltaTime;
        }
        else if (yVelocity <= 0)
        {
            yVelocity = -15;
        }

    }
    public virtual void AttemptJump()
    {
        if(!isGrounded)
        {
            return;
        }
        yVelocity = jumpHeight;

    }

    public void ApplyYVelocity(float newYVelocity)
    {
        yVelocity = newYVelocity;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckRaycastStart.position, groundCheckRadius);
    }
    #endregion
    public void ApplyMiscForce(Vector3 miscForce, float duration, AnimationCurve falloff)
    {
        //yComponent = 
        this.miscForce = miscForce;
        miscForceTimer = duration;
        planarMovement = miscForce;
    }
    void HandleMiscForces()
    {
        if(miscForceTimer > 0)
        {
            planarMovement -= planarMovement * airResistance;
            miscForceTimer -= Time.deltaTime;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        BouncePad bouncePad = other.GetComponent<BouncePad>();
        if(bouncePad != null)
        {
            ApplyYVelocity(bouncePad.bounciness);
        }
    }
}
