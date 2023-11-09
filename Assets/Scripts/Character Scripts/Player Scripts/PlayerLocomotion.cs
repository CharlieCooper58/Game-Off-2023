using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : CharacterLocomotion
{
    private PlayerManager playerManager;

    [Header("Wall Jumping and Sliding")]
    [SerializeField] float wallCheckRaycastDistance;
    Vector3 wallJumpDirection;
    [SerializeField] float wallJumpVelocityUp;
    [SerializeField] float wallJumpVelocitySide;

    [SerializeField] float wallJumpCooldownTimerMax;
    float wallJumpCooldownTimer;
    int wallJumpsPerformed;
    [SerializeField] int maxWallJumps;
    public override void Initialize()
    {
        base.Initialize();
        playerManager = GetComponent<PlayerManager>();
    }

    protected override Vector3 ProcessMovementInput(Vector2 moveInput)
    {
        return transform.forward * moveInput.y + transform.right * moveInput.x;
    }
    protected override void TickCooldownTimers(float tickLength)
    {
        base.TickCooldownTimers(tickLength);
        if (wallJumpCooldownTimer > 0)
        {
            wallJumpCooldownTimer = Mathf.Max(0, wallJumpCooldownTimer - tickLength);
        }
    }
    protected override void GroundedCheck()
    {
        base.GroundedCheck();
        if (isGrounded)
        {
            wallJumpCooldownTimer = 0;
            wallJumpsPerformed = 0;
        }
    }

    public override void AttemptJump()
    {
        if (isGrounded)
        {
            base.AttemptJump();
        }
        else
        {
            AttemptWallJump();
        }
    }
    private void AttemptWallJump()
    {
        if(wallJumpsPerformed >= maxWallJumps || wallJumpCooldownTimer > 0)
        {
            return;
        }
        wallJumpDirection = Vector3.zero;
        if (isCollidingForward)
        {
            wallJumpDirection -= transform.forward;
        }
        if (isCollidingBackward)
        {
            wallJumpDirection += transform.forward;
        }
        if (isCollidingRight)
        {
            wallJumpDirection -= transform.right;
        }
        if (isCollidingLeft)
        {
            wallJumpDirection += transform.right;
        }


        if (wallJumpDirection != Vector3.zero)
        {
            yVelocity = wallJumpVelocityUp;
            planarMovement = moveSpeed*wallJumpDirection.normalized;
            wallJumpCooldownTimer = wallJumpCooldownTimerMax;
            wallJumpsPerformed++;
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, wallCheckRaycastDistance * transform.forward);
    }
}
