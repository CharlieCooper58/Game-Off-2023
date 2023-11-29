using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class MortarProjectile : Projectile
{
    [SerializeField] GameObject hitMarkerCircle;
    [SerializeField] float explosionRadius;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] LayerMask groundMask;
    GameObject warningCircle;
    [SerializeField] EventReference onExplodeSound;
    public override void Initialize(Vector3 direction)
    {
        base.Initialize(direction);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 105f, groundMask))
        {
            warningCircle = Instantiate(hitMarkerCircle, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            warningCircle.transform.localScale = new Vector3(explosionRadius, 1, explosionRadius);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        AudioManager.instance.Play(onExplodeSound, transform.position);
        if (Vector3.SqrMagnitude(PlayerManager.littlePlayerInstance.transform.position - transform.position) < explosionRadius * explosionRadius)
        {
            PlayerManager.littlePlayerInstance.characterHealth.TakeDamage(damage);
        }
        Destroy(warningCircle.gameObject);
        Destroy(gameObject);
    }
}
