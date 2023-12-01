using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobPositionSetter : MonoBehaviour
{
    [SerializeField] CharacterLocomotion characterLocomotion;
    private Vector3 originalOffset;
    [SerializeField] Transform followTarget;
    public Vector3 offset;

    [SerializeField] float headBobAmplitudeX;
    [SerializeField] float headBobAmplitudeY;
    [SerializeField] float headBobFrequency;
    private Vector3 bobVelocity = Vector3.zero;
    float sinTime;
    bool isMovingGrounded;
    private void Start()
    {
        originalOffset = transform.localPosition;
    }
    private void Update()
    {
        if (characterLocomotion.isMovingGrounded)
        {
            sinTime += Time.deltaTime * headBobFrequency;
        }
        else
        {
            sinTime = 0f;
        }
        sinTime += Time.deltaTime * headBobFrequency;
        offset = (-Mathf.Abs(headBobAmplitudeY*Mathf.Sin(sinTime)))*Vector3.up+(Mathf.Sin(sinTime)*headBobAmplitudeY*headBobAmplitudeX)*Vector3.right;
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originalOffset + offset, ref bobVelocity, 0.3f);
    }
}
