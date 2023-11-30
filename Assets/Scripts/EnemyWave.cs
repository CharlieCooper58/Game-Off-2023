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
    List<Spawner> spawners;
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
        transform.localPosition = wavePosition;
        waveState = WaveState.unspawned;
        enemies = new List<EnemyManager> ();
        foreach (EnemyManager enemy in enemies)
        {
            if(enemy != null)
            {
                enemy.gameObject.SetActive(false);
                enemy.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
            }

        }
        spawners = GetComponentsInChildren<Spawner>().ToList();
        foreach (Spawner spawner in spawners)
        {
            spawner.GetComponent<SpawnerHealth>().OnSpawnerDeath += OnSpawnerDeath;
            spawner?.gameObject.SetActive(false);
        }
    }
    public void SpawnWave()
    {
        foreach (EnemyManager enemy in enemies)
        {
            if(enemy == null)
            {
                continue;
            }
            enemy?.gameObject.SetActive(true);
        }
        foreach (Spawner spawner in spawners)
        {
            if (spawner == null)
            {
                continue;
            }
            spawner?.gameObject.SetActive(true);
        }
    }
    public void TargetPlayer()
    {
        foreach (EnemyManager enemy in enemies)
        {
            if (enemy == null)
            {
                continue;
            }
            enemy.gameObject.SetActive(true);
            enemy.enemyAI.TargetPlayer();
        }
        foreach (Spawner spawner in spawners)
        {
            if(spawner == null)
            {
                continue;
            }
            spawner?.gameObject.SetActive(true);
            spawner?.SetEnemyTargets();
        }
    }

    public void PauseWave()
    {
        foreach(EnemyManager enemy in enemies)
        {
            if(enemy == null)
            {
                continue;
            }
            enemy.enemyAI.TargetPlants();
        }
    }
    public void ResumeWave()
    {
        foreach(EnemyManager enemy in enemies)
        {
            if(enemy == null)
            {
                continue;
            }
            enemy.enemyAI.TargetPlayer();
        }
    }


    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        enemies.Remove(e.manager as EnemyManager);
        if(spawners.Count == 0)
        {
            OnWaveComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DisposeOfEnemiesAndSpawners()
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner != null)
            {
                Destroy(spawner.gameObject);
            }
        }
        foreach (EnemyManager enemy in enemies)
        {
            if (enemy != null)
            {
                print(enemy.name);
                enemy.DisposeOfSelf();
            }
        }
       
    }

    private void OnSpawnerDeath(object sender, SpawnerHealth.SpawnerDeathEventArgs e)
    {
        spawners.Remove(e.deadSpawner);// GetComponent<Spawner>());
        if (spawners.Count == 0)
        {
            OnWaveComplete?.Invoke(this, EventArgs.Empty);
        }
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