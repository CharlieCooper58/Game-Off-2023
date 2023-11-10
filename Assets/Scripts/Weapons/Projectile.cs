using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    public void Initialize(Vector3 direction)
    {
        GetComponent<Rigidbody>().velocity = speed*direction.normalized;
        transform.LookAt(direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        print(gameObject.layer);
        CharacterHealth health = other.gameObject.GetComponent<CharacterHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);

    }
}
