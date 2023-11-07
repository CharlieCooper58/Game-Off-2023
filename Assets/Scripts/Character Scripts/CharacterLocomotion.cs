using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    private int moveSpeed;
    [SerializeField] private int groundedMoveSpeed;
    [SerializeField] private int inAirMoveSpeed;

    CharacterController controller;
    protected Vector3 moveDirection;

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
    [SerializeField] private const float gravity = 9.81f;
    protected Vector3 yVelocity;

    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] Transform groundCheckRaycastStart;
    [SerializeField] float groundCheckRadius = 1;
    [SerializeField] private int jumpHeight;
    public bool isGrounded;
    bool isJumping;

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
        HandleDashing();
        controller.Move((yVelocity + (moveSpeed * moveDirection)+dashVelocity) * Time.fixedDeltaTime);
    }


    // Gets information about this frame's movement.  For the player this will read from InputHandler, while for enemies it'll read from their AI
    protected virtual Vector3 ProcessMovementInput(Vector2 moveInput)
    {
        return moveInput;
    }

    public virtual void SetMoveDirection(Vector2 moveInput) 
    {
        moveDirection = ProcessMovementInput(moveInput);
    }


    #region Dashing and Charging
    public void AttemptDash()
    {
        if(dashTimer > 0 || dashCooldownTimer > 0)
        {
            return;
        }
        dashTimer = dashTimerMax;
        dashDirection = moveDirection;
    }
    private void HandleDashing()
    {
        if(dashTimer > 0)
        {
            dashVelocity = dashSpeed * dashCurve.Evaluate(dashTimer / dashTimerMax) * dashDirection;
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
            {
                dashVelocity = Vector3.zero;
                dashCooldownTimer = dashCooldownTimerMax;
            }
        }
        else if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }
    #endregion

    #region Jumping and Falling
    private void GroundedCheck()
    {
        if (Physics.CheckSphere(groundCheckRaycastStart.position, groundCheckRadius, groundCheckMask))
        {
            isGrounded = true;
            moveSpeed = groundedMoveSpeed;
        }
        else
        {
            isGrounded = false;
            moveSpeed = inAirMoveSpeed;
        }

    }

    private void HandleFalling()
    {
        if (!isGrounded)
        {
            yVelocity.y -= gravity*Time.deltaTime;
        }
        else if (yVelocity.y <= 0)
        {
            yVelocity.y = -2;
        }

    }
    public void AttemptJump()
    {
        if(!isGrounded)
        {
            return;
        }
        yVelocity.y = jumpHeight;

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckRaycastStart.position, groundCheckRadius);
    }
    #endregion

}
