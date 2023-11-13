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

    private void CharacterHealth_OnCharacterDeath(object sender, System.EventArgs e)
    {
        int num_organs = Random.Range(1, 4);
        for(int i = 0; i < num_organs; i++)
        {
            Instantiate(organs[Random.Range(0, organs.Length)], transform.position, Quaternion.identity);
        }
        Instantiate(splatter, transform.position, Quaternion.identity);
    }
}
