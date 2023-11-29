using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceFlicker : MonoBehaviour
{
    public float minIntensity = 0.5f; // Minimum intensity of the flicker
    public float maxIntensity = 1.5f; // Maximum intensity of the flicker
    public float flickerSpeed = 2.0f; // Speed of the flicker
    public float smoothness = 5.0f; // Smoothness factor

    private Light fireLight;
    private float baseIntensity;
    private float targetIntensity;

    void Start()
    {
        fireLight = GetComponent<Light>();
        baseIntensity = fireLight.intensity;
        targetIntensity = baseIntensity;
        // Invoke the Flicker function repeatedly
        InvokeRepeating("Flicker", 0.0f, flickerSpeed);
    }

    void Flicker()
    {
        // Calculate a random intensity value between min and max
        float randomIntensity = Random.Range(minIntensity, maxIntensity);
        // Set the target intensity to the random value
        targetIntensity = baseIntensity * randomIntensity;
    }

    void Update()
    {
        // Smoothly change the light intensity towards the target intensity
        fireLight.intensity = Mathf.Lerp(fireLight.intensity, targetIntensity, Time.deltaTime * smoothness);
    }
}