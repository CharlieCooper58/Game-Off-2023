using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    PlayerManager playerManager;
    private Vector2 movemementDirection;
    private Vector2 cameraDirection;
    private void OnEnable()
    {
        playerManager = GetComponent<PlayerManager>();
        PlayerControls inputActions = new PlayerControls();
        inputActions.Enable();
        inputActions.Movement.Movement.performed += x => movemementDirection = x.ReadValue<Vector2>();
        inputActions.Movement.Jump.performed += x=>OnJumpPerformed();
        inputActions.Movement.Dash.performed += x => OnDashPerformed();

        inputActions.Camera.CameraLook.performed += x=> cameraDirection = x.ReadValue<Vector2>();

        inputActions.Combat.Shoot.performed += x=>playerManager.playerAttacker.OnWeaponUsed();
        inputActions.Combat.Shoot.canceled += x=>playerManager.playerAttacker.OnWeaponReleased();
        inputActions.Combat.NextWeapon.performed += x=> playerManager.playerAttacker.CycleWeapons();
        // Don't ask me why it's 120, that's just the number that scrolling returns
        inputActions.Combat.CycleWeapons.performed += x=>playerManager.playerAttacker.CycleWeapons((int)x.ReadValue<float>()/120);
        inputActions.Combat.WeaponByNumber.performed += x => playerManager.playerAttacker.SwitchWeapon((int)x.ReadValue<float>()-1);

    }


    // Processes input on update and sends it to other components
    public void TickInput()
    {
        playerManager.characterLocomotion.SetMoveDirection(movemementDirection.normalized);
    }

    // Pulls input on LateUpdate for camera updates
    public void TickCameraInput()
    {
        playerManager.playerCamera.UpdateCameraOrientation(cameraDirection);
    }



    private void OnJumpPerformed() 
    {
        playerManager.characterLocomotion.AttemptJump();
    }
    private void OnDashPerformed()
    {
        playerManager.characterLocomotion.AttemptDash();
    }
}
