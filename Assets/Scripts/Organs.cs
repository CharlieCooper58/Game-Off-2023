using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organs : MonoBehaviour
{
    float despawnTime = 60;
    [SerializeField] float minSpeed = 5f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float maxAngle = 45f;
    [SerializeField] SplatterEffect splatterSystem;
    private void Start()
    {

        GetComponent<CharacterHealth>().OnCharacterDeath += Organs_OnCharacterDeath;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            float randomAngle = Random.Range(-maxAngle, maxAngle);
            Vector3 randomDirection = Quaternion.Euler(0, Random.Range(0f, 360f), 0) * Quaternion.Euler(randomAngle, 0, 0) * Vector3.up;
            float randomSpeed = Random.Range(minSpeed, maxSpeed);

            // Apply force with a random direction and speed
            rb.AddForce(randomDirection * randomSpeed, ForceMode.Impulse);
        }
        StartCoroutine("DespawnOrgans");
    }

    private void Organs_OnCharacterDeath(object sender, System.EventArgs e)
    {
        Instantiate(splatterSystem, transform.position, Quaternion.identity);
    }

    private IEnumerator DespawnOrgans()
    {
        yield return new WaitForSeconds(despawnTime);
        GetComponent<CharacterHealth>()?.TakeDamage(1000);
    }
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<CharacterHealth>()?.TakeDamage(1000);
    }
}
