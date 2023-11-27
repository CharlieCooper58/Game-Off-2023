using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarWeapon : Weapon
{
    [SerializeField] MortarProjectile proj;
    Vector3 targetPos;
    [SerializeField] public float height = 10f;
    public override bool OnTriggerPressed(Vector3 direction)
    {
        if (reloadTimer > 0)
        {
            return false;
        }
        //Instantiate projectile above target, falling down.
        MortarProjectile newProj = Instantiate(proj, direction + Vector3.up*height, Quaternion.identity);
        newProj.Initialize(Vector3.down);
        return true;
    }
}
