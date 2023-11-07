using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PesticideSprayer : Weapon
{
    bool firing = false;

    float particleSystemDistance;
    float baseParticleLifetime;

    ParticleSystem.MainModule mainModule;
    

    [SerializeField] LayerMask spherecastMask;
    [SerializeField] float spherecastRadius;

    [SerializeField] LayerMask damageMask;
    [SerializeField] float damageRadius;

    private void Start()
    {
        mainModule = weaponParticles.main;
        baseParticleLifetime = weaponParticles.main.startLifetime.constant;
        particleSystemDistance = baseParticleLifetime * weaponParticles.main.startSpeed.constant;
        weaponParticles.Stop();
    }
    protected override void Update()
    {
        base.Update();
        if (firing)
        {
            FirePesticideSpray();
        }
    }
    public override bool OnTriggerPressed()
    {
        firing = true;
        weaponParticles.Play();
        return true;
    }
    public override void OnTriggerReleased()
    {
        firing = false;
        weaponParticles.Stop();
        base.OnTriggerReleased();
    }

    private void FirePesticideSpray()
    {
        RaycastHit hit;
        if (Physics.SphereCast(muzzleEndpoint.position, spherecastRadius, transform.forward, out hit, particleSystemDistance, spherecastMask))
        {
            mainModule.startLifetimeMultiplier = hit.distance / particleSystemDistance;
            if(reloadTimer == 0)
            {
                Collider[] ObjectsInHitRadius = Physics.OverlapSphere(hit.point, damageRadius, damageMask);
                foreach (Collider enemyHit in ObjectsInHitRadius)
                {
                    CharacterHealth health = enemyHit.GetComponent<CharacterHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(weaponDamage);
                    }
                }

                reloadTimer = reloadTimerMax;
            }
            
        }
        else
        {
            mainModule.startLifetimeMultiplier = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(muzzleEndpoint.position, damageRadius);
    }
}
