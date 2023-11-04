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

    protected override Vector3 ProcessMovementInput(Vector2 moveInput)
    {
        return transform.forward * moveInput.y + transform.right * moveInput.x;
    }
}
