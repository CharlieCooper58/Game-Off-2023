using System.Collections;
using UnityEngine;

public class SplatterEffect : MonoBehaviour
{
    private ParticleSystem m_particleSystem;

    private void Awake()
    {
        m_particleSystem = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(WaitAndDestroy());
        m_particleSystem.Play();
    }

    private IEnumerator WaitAndDestroy()
    {
        // Wait until the particle system finishes playing
        while (m_particleSystem.IsAlive(true))
        {
            yield return null;
        }

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
