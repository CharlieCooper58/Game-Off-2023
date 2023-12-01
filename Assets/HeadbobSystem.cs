using System.Collections;
using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    public float bobFrequency = 2.5f; // Adjust this to control the frequency of the bobbing
    public float bobAmount = 0.1f; // Adjust this to control the intensity of the bobbing

    private Transform cameraPivotTransform;
    private Vector3 originalCameraPosition;
    private float timer = 0f;

    void Start()
    {
        cameraPivotTransform = transform; // Assuming the script is on the camera pivot GameObject
        originalCameraPosition = cameraPivotTransform.localPosition;
    }

    void Update()
    {
        // Assuming you have a character controller attached to your player GameObject
        CharacterController characterController = GetComponent<CharacterController>();
        if (characterController.isGrounded && characterController.velocity.magnitude > 0)
        {
            BobCameraPivot();
        }
        else
        {
            // Reset camera pivot position if not moving
            ResetCameraPivotPosition();
        }
    }

    void BobCameraPivot()
    {
        float waveSlice = Mathf.Sin(timer);
        Vector3 targetCameraPivotPosition = originalCameraPosition + new Vector3(0, waveSlice * bobAmount, 0);

        timer += bobFrequency * Time.deltaTime;

        if (timer > Mathf.PI * 2)
        {
            timer = timer - (Mathf.PI * 2);
        }
    }

    void ResetCameraPivotPosition()
    {
        // Reset camera pivot position when not moving
        timer = 0f;
    }
}
