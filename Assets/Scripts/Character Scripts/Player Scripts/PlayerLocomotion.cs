using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : CharacterLocomotion
{
    private PlayerManager playerManager;


    public override void Initialize()
    {
        base.Initialize();
        playerManager = GetComponent<PlayerManager>();
    }

    protected override void GetMovementInformation()
    {
        Vector2 inputDirection = playerManager.inputHandler.GetMovementDirection();

        moveDirection = transform.forward*inputDirection.y + transform.right*inputDirection.x;
    }
}
