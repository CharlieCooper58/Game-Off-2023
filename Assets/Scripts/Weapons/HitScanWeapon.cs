using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HitScanWeapon : Weapon
{
    // A hitscan weapon is a weapon that raycasts from the barrel to the target and sees if anything hits

    [SerializeField] private EventReference GunshotSound;

    [SerializeField] HitScanLine shotTracerLinePrefab;
    [SerializeField] LayerMask shootMask;
    public override bool OnTriggerPressed()
    {
        if(reloadTimer == 0)
        {
            AudioManager.instance.PlayOneShot(GunshotSound, this.transform.position);

            reloadTimer = reloadTimerMax;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, 1000f, layerMask:shootMask))
            {
                HitScanLine newShotLine = Instantiate(shotTracerLinePrefab);
                newShotLine.CreateLine(muzzleEndpoint.position, hit.point);
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

                HitScanLine newShotLine = Instantiate(shotTracerLinePrefab);
                newShotLine.CreateLine(muzzleEndpoint.position, pointToDraw);
            }
            return true;
        }
        return false;
    }
}

