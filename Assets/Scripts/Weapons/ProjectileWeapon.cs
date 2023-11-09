using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    Transform muzzlePosition;
    Projectile proj;
    public override bool OnTriggerPressed(Vector3 direction)
    {
        if (reloadTimer > 0)
        {
            return false;
        }
        Projectile newProj = Instantiate(proj, muzzlePosition.position, Quaternion.identity);
        newProj.Initialize(direction);
        return true;
    }
}
