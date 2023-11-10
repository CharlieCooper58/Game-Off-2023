using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon:MonoBehaviour
{
    [SerializeField] public Transform muzzleEndpoint;
    protected float reloadTimer;
    [SerializeField] protected float reloadTimerMax;
    [SerializeField] protected ParticleSystem weaponParticles;
    [SerializeField] public int weaponDamage;

    protected virtual void Update()
    {
        if(reloadTimer > 0)
        {
            reloadTimer = Mathf.Max(reloadTimer - Time.deltaTime, 0);
        }
    }
    public virtual bool OnTriggerPressed() 
    {
        return true;
    }
    public virtual bool OnTriggerPressed(Vector3 direction)
    {
        return true;
    }
    public virtual void OnTriggerReleased() { }
}
