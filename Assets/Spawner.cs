using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Spawner : MonoBehaviour
{
    [SerializeField] int maxEnemies;
    [SerializeField] int minEnemies;

    float enemySpawnTimer;
    [SerializeField] float enemySpawnTimerMax;

    [SerializeField] EnemyManager[] spawnableEnemies;
    List<EnemyManager> enemies;
    int enemiesSpawned;
    bool spawning;
    bool targetingPlayer;

    [SerializeField] GameObject plantTarget;
    EnemyWave wave;

    private void Start()
    {
        enemies = new List<EnemyManager>();
        wave = GetComponentInParent<EnemyWave>();
        //foreach(EnemyManager enemy in enemies)
        //{
        //    enemy.gameObject.SetActive(true);
        //    enemy.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
        //}
        spawning = true;
    }
    public void SetEnemyTargets()
    {
        targetingPlayer = true;
        foreach (EnemyManager enemy in enemies)
        {
            enemy.enemyAI.TargetPlayer();
        }
    }

    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        enemies.Remove(e.manager as EnemyManager);
        if(!spawning && enemies.Count < minEnemies)
        {
            spawning = true;
        }
    }

    private void Update()
    {
        if(spawning)
        {
            enemySpawnTimer -= Time.deltaTime;
            if(enemySpawnTimer <= 0)
            {
                SpawnEnemy();
                enemySpawnTimer = enemySpawnTimerMax;
                if (enemies.Count >= maxEnemies)
                {
                    spawning=false;
                }
            }
        }
    }
    private void SpawnEnemy()
    {
        EnemyManager newEnemy = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length)], transform.position, Quaternion.identity);
        enemies.Add(newEnemy);
        wave.AddNewEnemy(newEnemy);
        newEnemy.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
    }
}
