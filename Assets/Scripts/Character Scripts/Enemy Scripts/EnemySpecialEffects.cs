using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpecialEffects : MonoBehaviour
{
    [SerializeField] Organs[] organs;
    [SerializeField] SplatterEffect splatter;


    EnemyManager manager;
    public void Initialize()
    {
        manager = GetComponent<EnemyManager>();
        manager.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
    }

    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        int numOrgans;
        numOrgans = e.isBrutal? Random.Range(5, 8):0;
        print(numOrgans);
        for(int i = 0; i < numOrgans; i++)
        {
            Instantiate(organs[Random.Range(0, organs.Length)], transform.position, Quaternion.identity);
        }
        Instantiate(splatter, transform.position, Quaternion.identity);
    }
}
