using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class FireEffect : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float flickerSpeed = 1f;

    float random;
    [SerializeField] EventReference logCrackle;

    void Start()
    {
        if (fireLight == null)
        {
            Debug.LogError("Fire light not assigned!");
            return;
        }
        AudioManager.instance.Play(logCrackle, transform.position);
        
        // Store a random value to add variation to the flickering
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        // Calculate flickering effect using Perlin noise
        float noise = Mathf.PerlinNoise(random, Time.time * flickerSpeed);
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);

        // Apply the flickering intensity to the light
        fireLight.intensity = intensity;
    }
}
