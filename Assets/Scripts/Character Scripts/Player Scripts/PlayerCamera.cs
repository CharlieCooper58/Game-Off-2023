using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPivot;
    CinemachineVirtualCamera camera;
    PlayerManager playerManager;

    float cameraXAxisRotation;

    [SerializeField] float cameraSpeedX;
    [SerializeField] float cameraSpeedY;
    public void Initialize()
    {
        camera = GetComponentInChildren<CinemachineVirtualCamera>();
        playerManager = GetComponent<PlayerManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UpdateCameraOrientation()
    {
        print("Blah");
        Vector2 cameraInput = playerManager.inputHandler.GetCameraDirection();
        float lookX = cameraSpeedX*cameraInput.x*Time.deltaTime;
        float lookY = cameraSpeedY * cameraInput.y * Time.deltaTime;

        cameraXAxisRotation = Mathf.Clamp(cameraXAxisRotation - lookY, -90f, 90f);

        transform.Rotate(Vector3.up * lookX);
        cameraPivot.localRotation = Quaternion.Euler(cameraXAxisRotation, 0, 0);

    }
}
