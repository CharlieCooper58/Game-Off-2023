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

    [SerializeField] Transform cameraRotationHelper;
    float targetCameraXAxisRotation;

    float cameraXAngularVelocity;
    float cameraYAngularVelocity;

    [SerializeField] float cameraSmoothTime;

    [SerializeField] float cameraSpeedX;
    [SerializeField] float cameraSpeedY;
    public void Initialize()
    {
        camera = GetComponentInChildren<CinemachineVirtualCamera>();
        playerManager = GetComponent<PlayerManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraRotationHelper.localRotation = transform.rotation;
    }
    public void UpdateCameraOrientation(Vector2 cameraInput)
    {
        float lookX = cameraSpeedX * cameraInput.x * Time.deltaTime;
        float lookY = cameraSpeedY * cameraInput.y * Time.deltaTime;

        // Adjust the target camera rotation based on input
        targetCameraXAxisRotation = Mathf.Clamp(targetCameraXAxisRotation - lookY, -90f, 90f);
        cameraRotationHelper.Rotate(Vector3.up * lookX);// = transform.eulerAngles.y + lookX;
        //cameraRotationHelper.localRotation.x = targetCameraXAxisRotation;

        // Update the camera pivot and player's transform rotation
        cameraPivot.localRotation = Quaternion.Euler(Mathf.SmoothDampAngle(cameraPivot.localEulerAngles.x, targetCameraXAxisRotation, ref cameraXAngularVelocity, cameraSmoothTime), 0, 0);
        transform.rotation = Quaternion.Euler(0, Mathf.SmoothDampAngle(transform.eulerAngles.y, cameraRotationHelper.localEulerAngles.y, ref cameraYAngularVelocity, cameraSmoothTime), 0);
    }

    private void OnDisable()
    {
        camera.Priority = 0;
    }
    private void OnEnable()
    {
        if(camera != null)
        {
            camera.Priority = 10;
        }
    }
}
