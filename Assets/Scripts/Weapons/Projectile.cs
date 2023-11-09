using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int damage;
    float speed;
    public void Initialize(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = speed*direction;
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterHealth health = other.GetComponent<CharacterHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
