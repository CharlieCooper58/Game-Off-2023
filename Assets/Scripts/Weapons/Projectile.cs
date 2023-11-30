using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] float speed;
    [SerializeField] EventReference projectileHitSound;
    public virtual void Initialize(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = speed*direction.normalized;
        transform.LookAt(direction);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {

        CharacterHealth health = other.gameObject.GetComponent<CharacterHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            AudioManager.instance.Play(projectileHitSound, transform.position);
        }
        Destroy(gameObject);

    }
}
