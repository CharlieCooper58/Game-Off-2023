using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] float speed;
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
        }
        Destroy(gameObject);

    }
}
