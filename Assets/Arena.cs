using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arena : MonoBehaviour
{
    [SerializeField] EnemyWave[] waves;
    int waveCount;
    EnemyWave currentWave;
    private void TriggerNextWave()
    {
        if (waveCount > waves.Length - 1)
        {
            OnArenaComplete();
        }
        else
        {
            currentWave = Instantiate(waves[waveCount]);
            currentWave.SpawnWave();
            waveCount++;
            currentWave.OnWaveComplete += NewWave_OnWaveComplete;
        }
    }
    private void NewWave_OnWaveComplete(object sender, System.EventArgs e)
    {
        Destroy(currentWave.gameObject);
        TriggerNextWave();
    }
    void OnArenaComplete()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Arenas sit on the Level Progression layer, which ONLY touches the Player layer
        if(waveCount == 0) TriggerNextWave();
    }
}