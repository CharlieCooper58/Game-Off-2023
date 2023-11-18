using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerRotation : MonoBehaviour
{
    public float rotationAmount = 10f; // Adjust the rotation amount based on your preference
    public float rotationSpeed = 1f;   // Adjust the rotation speed based on your preference

    private bool isRotatingForward = true;
    private float startRotation;

    void Start()
    {
        startRotation = Random.Range(0f, 360f); // Random start rotation between 0 and 360 degrees
        transform.Rotate(Vector3.up, startRotation);
    }

    void Update()
    {
        //RotateSunflower();
    }

    void RotateSunflower()
    {
        float rotation = rotationAmount * Mathf.Sin(Time.time * rotationSpeed);

        if (isRotatingForward)
        {
            transform.Rotate(Vector3.up, rotation * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up, -rotation * Time.deltaTime);
        }

        if (Mathf.Abs(rotation) >= rotationAmount)
        {
            isRotatingForward = !isRotatingForward;
        }
    }
}
