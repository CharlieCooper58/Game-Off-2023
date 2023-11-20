using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaStartTrigger : MonoBehaviour
{
    EnemyWave enemyWave;
    private void Awake()
    {
        enemyWave = GetComponentInParent<EnemyWave>();
    }
    private void OnTriggerEnter(Collider other)
    {
        enemyWave.OnPlayerEnter();
        Destroy(gameObject);
    }
}
