using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using FMODUnity;
using FMOD.Studio;

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
    [SerializeField] private EventReference weaponSound;
    [SerializeField] private EventReference enemyBurnSound;

    [SerializeField] GameObject pesticideParticles;
    EventInstance weaponSoundEvent;
    private void Start()
    {
        mainModule = weaponParticles.main;
        baseParticleLifetime = weaponParticles.main.startLifetime.constant;
        particleSystemDistance = baseParticleLifetime * weaponParticles.main.startSpeed.constant;
        weaponParticles.Stop();
        weaponSoundEvent = AudioManager.instance.Play(weaponSound, this.transform.position);
        weaponSoundEvent.setParameterByName("SprayGun", 1);
        weaponSoundEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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
        weaponSoundEvent.start();
        weaponSoundEvent.setParameterByName("SprayGun", 0);

        return true;
    }
    public override void OnTriggerReleased()
    {
        firing = false;
        weaponParticles.Stop();
        //weaponSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        weaponSoundEvent.setParameterByName("SprayGun", 1);

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
                Instantiate(pesticideParticles, hit.point, Quaternion.identity);
                Collider[] ObjectsInHitRadius = Physics.OverlapSphere(hit.point, damageRadius, damageMask);
                foreach (Collider enemyHit in ObjectsInHitRadius)
                {
                    CharacterHealth health = enemyHit.GetComponent<CharacterHealth>();
                    if (health != null)
                    {
                        health.TakeDamage(weaponDamage);
                        AudioManager.instance.Play(enemyBurnSound, health.transform.position);
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
