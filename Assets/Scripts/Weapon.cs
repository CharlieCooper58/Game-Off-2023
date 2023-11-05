using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon:MonoBehaviour
{
    [SerializeField] protected Transform muzzleEndpoint;
    protected float reloadTimer;
    [SerializeField] protected float reloadTimerMax;
    [SerializeField] private ParticleSystem weaponParticles;
    [SerializeField] protected int weaponDamage;

    private void Update()
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
    public virtual void OnTriggerReleased() { }
}
