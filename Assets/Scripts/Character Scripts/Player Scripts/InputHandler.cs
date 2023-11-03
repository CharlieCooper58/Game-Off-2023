using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Vector2 movemementDirection;
    private Vector2 cameraDirection;
    private void OnEnable()
    {
        PlayerControls inputActions = new PlayerControls();
        inputActions.Enable();
        inputActions.Movement.Movement.performed += x=> movemementDirection = x.ReadValue<Vector2>();
        inputActions.Movement.Jump.performed += x=>OnJumpPerformed();
        inputActions.Camera.CameraLook.performed += x=> cameraDirection = x.ReadValue<Vector2>();
    }

    // Takes input and distributes it to other player components
    public Vector2 GetMovementDirection()
    {
        return movemementDirection.normalized;
    }
    public Vector2 GetCameraDirection()
    {
        return cameraDirection;
    }

    private void OnJumpPerformed() { }
}
