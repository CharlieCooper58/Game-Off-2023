using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PesticideSprayer: Weapon
{
    bool firing = false;
    [SerializeField] Collider hitCollider;
    List<CharacterHealth> hitObjects;


    private void Start()
    {
        hitCollider.enabled = false;
        hitObjects = new List<CharacterHealth>();
        //weaponParticles.Stop();
    }
    protected override void Update()
    {
        base.Update();
        if(firing && reloadTimer == 0)
        {
            if(hitObjects.Count>0)
            {
                reloadTimer = reloadTimerMax;
                foreach (CharacterHealth enemy in hitObjects)
                {
                    if(enemy != null)
                    {
                        enemy.TakeDamage(weaponDamage);
                    }
                }
            }
            

        }
    }
    public override bool OnTriggerPressed()
    {
        firing = true;
        weaponParticles.Play();
        hitCollider.enabled = true;
        return true;
    }
    public override void OnTriggerReleased()
    {
        firing=false;
        weaponParticles.Stop();
        hitCollider.enabled = false;
        hitObjects.Clear();
        base.OnTriggerReleased();
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterHealth enemy = other.GetComponent<CharacterHealth>();
        if(enemy != null)
        {
            hitObjects.Add(enemy);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        CharacterHealth enemy = other.GetComponent<CharacterHealth>();
        if (hitObjects.Contains(enemy))
        {
            hitObjects.Remove(enemy);
        }
    }
}
