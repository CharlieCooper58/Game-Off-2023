using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float inAirMovementDelta;


    CharacterController controller;
    float controllerTopOffset;
    float controllerLateralOffset;
    protected bool isCollidingUp;
    protected bool isCollidingLeft;
    protected bool isCollidingRight;
    protected bool isCollidingForward;
    protected bool isCollidingBackward;

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
    [SerializeField] float frictionModifier;
    public bool isGrounded;
    bool isJumping;

    //[Header("Misc Forces")]
    float miscForceTimer;
    [SerializeField] float airResistance;

    // Initialize is called by the CharacterManager
    public virtual void Initialize()
    {
        controller = GetComponent<CharacterController>();
        controllerTopOffset = controller.height/2;
        controllerLateralOffset = controller.radius;

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
        controller.Move((yVelocity * transform.up + planarMovement) * Time.deltaTime);
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
    protected virtual void GroundedCheck()
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

    private void HandleFalling()
    {
        if (!isGrounded)
        {
            yVelocity -= gravity*Time.deltaTime;

            // Somewhat costly collision detection.  Used for wall jumps, canceling velocity, and sliding
            isCollidingForward = CheckCollisionDirection(transform.forward);
            isCollidingBackward = CheckCollisionDirection(-transform.forward);
            isCollidingRight = CheckCollisionDirection(transform.right);
            isCollidingLeft = CheckCollisionDirection(-transform.right);
            isCollidingUp = CheckCollisionDirection(transform.up);

            if(isCollidingBackward || isCollidingForward || isCollidingLeft || isCollidingRight)
            {
                yVelocity -= yVelocity * frictionModifier*Time.deltaTime;
            }
        }
        else
        {
            isCollidingForward = isCollidingBackward = isCollidingRight = isCollidingLeft = isCollidingUp = false;
            if (yVelocity <= 0)
            {
                yVelocity = -15;
            }
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
    public void ApplyMiscForce(Vector3 miscForce, float duration)
    {
        print(miscForce);
        float yComponent = Vector3.Dot(miscForce, Vector3.up);
        ApplyYVelocity(yComponent);

        planarMovement = miscForce - yComponent * Vector3.up;
        miscForceTimer = duration;
        print(planarMovement);
    }
    void HandleMiscForces()
    {
        if(miscForceTimer > 0)
        {
            print(planarMovement);
            planarMovement -= planarMovement * airResistance*Time.deltaTime;
            miscForceTimer -= Time.deltaTime;
        }
    }

    protected bool CheckCollisionDirection(Vector3 direction)
    {
        if(direction.y != 0)
        {
            return Physics.Raycast(transform.position+direction*controllerTopOffset, direction, 0.2f, groundCheckMask);
        }
        else
        {
            return Physics.Raycast(transform.position + direction * controllerLateralOffset, direction, 0.2f, groundCheckMask);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // X
        if (planarMovement.x != 0)
        {
            if(Vector3.Dot(planarMovement, transform.right) > 0 && isCollidingRight || (Vector3.Dot(planarMovement, -transform.right) > 0 && isCollidingLeft))
            {
                planarMovement.x = 0;
            }
        }

        // Z
        if (planarMovement.z != 0)
        {
            if (Vector3.Dot(planarMovement, transform.forward) > 0 && isCollidingForward || (Vector3.Dot(planarMovement, -transform.forward) > 0 && isCollidingBackward))
            {
                planarMovement.z = 0;
            }
        }

        // Y
        if (yVelocity > 0 && isCollidingUp)
        {
            yVelocity = 0;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        BouncePad bouncePad = other.GetComponent<BouncePad>();
        if(bouncePad != null)
        {
            ApplyMiscForce(bouncePad.bounciness*bouncePad.transform.up, bouncePad.bounceDuration);
        }
    }
}
