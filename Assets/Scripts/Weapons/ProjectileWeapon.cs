using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    [SerializeField] Projectile proj;
    public override bool OnTriggerPressed(Vector3 direction)
    {
        if (reloadTimer > 0)
        {
            return false;
        }
        Projectile newProj = Instantiate(proj, muzzleEndpoint.position, Quaternion.identity);
        newProj.Initialize(direction);
        return true;
    }
}
