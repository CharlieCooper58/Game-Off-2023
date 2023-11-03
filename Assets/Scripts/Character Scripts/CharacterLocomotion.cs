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


    [Header("Jumping and Falling")]
    protected float gravity;
    protected Vector3 yVelocity;

    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] Transform groundCheckRaycastStart;
    [SerializeField] float groundCheckRadius = 1;
    [SerializeField] private int jumpHeight;
    bool isGrounded;

    // Initialize is called by the CharacterManager
    public virtual void Initialize()
    {
        controller = GetComponent<CharacterController>();
        groundCheckRadius = controller.radius;
    }
    public void HandleAllMovement()
    {
        GetMovementInformation();
        GroundedCheck();

        controller.Move((yVelocity + (moveSpeed * moveDirection)) * Time.fixedDeltaTime);
    }

    // Gets information about this frame's movement.  For the player this will read from InputHandler, while for enemies it'll read from their AI
    protected virtual void GetMovementInformation(){}


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
            yVelocity.y -= gravity;
        }
        else
        {
            yVelocity.y = 0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckRaycastStart.position, groundCheckRadius);
    }
    #endregion

}
