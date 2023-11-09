using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanWeapon : Weapon
{
    // A hitscan weapon is a weapon that raycasts from the barrel to the target and sees if anything hits

    [SerializeField] LineRenderer shotTracerRenderer;
    [SerializeField] LayerMask shootMask;
    public override bool OnTriggerPressed()
    {
        if(reloadTimer == 0)
        {
            reloadTimer = reloadTimerMax;
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, shootMask))
            {
                shotTracerRenderer.enabled = true;
                shotTracerRenderer.positionCount = 2;
                shotTracerRenderer.SetPosition(0, muzzleEndpoint.position);
                shotTracerRenderer.SetPosition(1, hit.point);
                StartCoroutine("DisableShotTracer");
                CharacterHealth damaged = hit.transform.GetComponent<CharacterHealth>();
                if(damaged != null)
                {
                    damaged.TakeDamage(weaponDamage);
                }
            }
            else
            {
                // Raycast did not hit anything, draw a point 100 units away in the ray's direction
                Vector3 raycastEndpoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1.0f));
                Vector3 pointToDraw = raycastEndpoint + Camera.main.transform.forward * 100f;

                shotTracerRenderer.enabled = true;
                shotTracerRenderer.positionCount = 2;
                shotTracerRenderer.SetPosition(0, muzzleEndpoint.position);
                shotTracerRenderer.SetPosition(1, pointToDraw);
                StartCoroutine("DisableShotTracer");
            }
            return true;
        }
        return false;
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

        shotTracerRenderer.enabled = false;
    }
}

