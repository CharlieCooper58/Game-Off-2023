using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarWeapon : Weapon
{
    [SerializeField] Projectile proj;
    Vector3 targetPos;
    [SerializeField] float height = 10f;
    public override bool OnTriggerPressed(Vector3 direction)
    {
        if (reloadTimer > 0)
        {
            return false;
        }
        //Instantiate projectile above target, falling down.
        Projectile newProj = Instantiate(proj, targetPos + Vector3.up*height, Quaternion.identity);
        newProj.Initialize(Vector3.down);
        return true;
    }
}
