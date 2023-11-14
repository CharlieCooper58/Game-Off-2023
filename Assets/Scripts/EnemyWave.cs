using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyWave : MonoBehaviour
{
    [TextArea] public string waveDescription;


    public Vector3 wavePosition;
    int n_enemies;
    public event EventHandler OnWaveComplete;
    public void SpawnWave()
    {
        transform.position = wavePosition;
        EnemyManager[] enemies = GetComponentsInChildren<EnemyManager>();
        foreach (EnemyManager enemy in enemies)
        {
            print("Enemy");
            n_enemies++;
            enemy.gameObject.SetActive(true);
            enemy.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;

        }
    }

    private void CharacterHealth_OnCharacterDeath(object sender, EventArgs e)
    {
        print("Died");
        n_enemies--;
        if(n_enemies == 0)
        {
            OnWaveComplete.Invoke(this, EventArgs.Empty);
        }
    }

}