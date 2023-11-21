using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class EnemyWave : MonoBehaviour
{
    [TextArea] public string waveDescription;


    public Vector3 wavePosition;
    List<EnemyManager> enemies;
    Spawner[] spawners;
    public event EventHandler OnWaveComplete;

    [SerializeField] Collider firstTrigger;
    [SerializeField] Collider secondTrigger;
    enum WaveState
    {
        unspawned,
        spawned,
        attacking
    }
    WaveState waveState;
    private void Start()
    {
        waveState = WaveState.unspawned;
        enemies = GetComponentsInChildren<EnemyManager>().ToList();
        foreach (EnemyManager enemy in enemies)
        {
            if(enemy != null)
            {
                enemy.gameObject.SetActive(false);
                enemy.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
            }

        }
        spawners = GetComponentsInChildren<Spawner>();
        foreach (Spawner spawner in spawners)
        {
            spawner?.gameObject.SetActive(false);
        }
    }
    public void SpawnWave()
    {
        foreach (EnemyManager enemy in enemies)
        {
            enemy?.gameObject.SetActive(true);
        }
        foreach (Spawner spawner in spawners)
        {
            spawner?.gameObject.SetActive(true);
        }
    }
    public void TargetPlayer()
    {
        foreach (EnemyManager enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
            enemy.enemyAI.TargetPlayer();
        }
        foreach (Spawner spawner in spawners)
        {
            spawner?.gameObject.SetActive(true);
            spawner?.SetEnemyTargets();
        }
    }


    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        enemies.Remove(e.manager as EnemyManager);
    }

    public void AddNewEnemy(EnemyManager newEnemy)
    {
        enemies.Add(newEnemy);
        newEnemy.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
        if (waveState == WaveState.attacking)
        {
            newEnemy.enemyAI.TargetPlayer();
        }
    }

    public void OnPlayerEnter()
    {
        if (waveState == WaveState.unspawned)
        {
            SpawnWave();
            waveState = WaveState.spawned;
        }
        else
        {
            waveState = WaveState.attacking;
            TargetPlayer();
        }        
    }

}