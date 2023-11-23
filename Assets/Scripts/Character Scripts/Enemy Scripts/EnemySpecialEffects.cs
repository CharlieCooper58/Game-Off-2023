using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnemySpecialEffects : MonoBehaviour
{
    [SerializeField] private EventReference BugDeath;
    [SerializeField] private EventReference BugHurt;
    [SerializeField] Organs[] organs;
    [SerializeField] SplatterEffect splatter;


    EnemyManager manager;
    public void Initialize()
    {
        manager = GetComponent<EnemyManager>();
        manager.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
        manager.characterHealth.OnCharacterHit += CharacterHealth_OnCharacterHit;
    }

    private void CharacterHealth_OnCharacterHit(object sender, System.EventArgs e) {
        AudioManager.instance.PlayOneShot(BugHurt, this.transform.position);
    }

    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        int numOrgans;
        numOrgans = e.isBrutal? Random.Range(5, 8):0;
        for(int i = 0; i < numOrgans; i++)
        {
            Instantiate(organs[Random.Range(0, organs.Length)], transform.position, Quaternion.identity);
        }
        AudioManager.instance.PlayOneShot(BugDeath, this.transform.position);
        Instantiate(splatter, transform.position, Quaternion.identity);

    }
}
