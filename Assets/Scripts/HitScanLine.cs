using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanLine : MonoBehaviour
{
    // A hitscan weapon is a weapon that raycasts from the barrel to the target and sees if anything hits

    [SerializeField] LineRenderer shotTracerRenderer;

    public void CreateLine(Vector3 start, Vector3 end)
    {
        shotTracerRenderer.positionCount = 2;
        shotTracerRenderer.SetPosition(0, start);
        shotTracerRenderer.SetPosition(1, end);
        StartCoroutine("DisableShotTracer");
    }

    IEnumerator DisableShotTracer()
    {
        float elapsedTime = 0.0f;
        Color newColor = shotTracerRenderer.startColor;

        while (elapsedTime < 0.3f)
        {
            // Calculate the new color with reduced alpha
            newColor.a = 1.0f - (elapsedTime / .3f);

            // Set the LineRenderer color
            shotTracerRenderer.startColor = newColor;
            shotTracerRenderer.endColor = newColor;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        Destroy(gameObject);
    }
}

