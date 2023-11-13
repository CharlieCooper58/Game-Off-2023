using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerManager playerManager;
    private Vector2 movemementDirection;
    private Vector2 cameraDirection;

    PlayerControls inputActions;
    private void OnEnable()
    {
        playerManager = GetComponent<PlayerManager>();
        if(inputActions == null)
        {
            inputActions = new PlayerControls();

            // If the player is little, they have access to a different set of actions than if they're big
            inputActions.Movement.Movement.performed += x => movemementDirection = x.ReadValue<Vector2>();
            inputActions.Movement.Jump.performed += x => OnJumpPerformed();


            inputActions.Camera.CameraLook.performed += x => cameraDirection = x.ReadValue<Vector2>();

            if (playerManager.isLittle)
            {
                inputActions.Movement.Dash.performed += x => playerManager.playerLocomotion.AttemptDash(movemementDirection.normalized);

                inputActions.Combat.Shoot.performed += x => playerManager.playerAttacker.OnWeaponUsed();
                inputActions.Combat.Shoot.canceled += x => playerManager.playerAttacker.OnWeaponReleased();
                inputActions.Combat.NextWeapon.performed += x => playerManager.playerAttacker.CycleWeapons();
                // Don't ask me why it's 120, that's just the number that scrolling returns
                inputActions.Combat.CycleWeapons.performed += x => playerManager.playerAttacker.CycleWeapons((int)x.ReadValue<float>() / 120);
                inputActions.Combat.WeaponByNumber.performed += x => playerManager.playerAttacker.SwitchWeapon((int)x.ReadValue<float>() - 1);
            }
        }
        inputActions.Enable();
        
        

    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    private void OnDestroy()
    {
        inputActions.Dispose();
    }


    // Processes input on update and sends it to other components
    public void TickInput()
    {
        playerManager.playerLocomotion.SetMoveDirection(movemementDirection.normalized);
    }

    // Pulls input on LateUpdate for camera updates
    public void TickCameraInput()
    {
        playerManager.playerCamera.UpdateCameraOrientation(cameraDirection);
    }



    private void OnJumpPerformed() 
    {
        playerManager.playerLocomotion.AttemptJump();
    }

}
