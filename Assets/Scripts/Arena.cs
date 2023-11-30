using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Arena : MonoBehaviour
{
    [SerializeField] EnemyWave[] waves;
    List<EnemyWave> spawnedWaves;

    public bool arenaIsComplete;
    int wavesCompleted;

    private void Awake()
    {
        spawnedWaves = new List<EnemyWave>();
    }
    private void Start()
    {
        PlayerManager.littlePlayerInstance.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
        GameHandler.instance.OnPlayerSizeChange += Instance_OnPlayerSizeChange;
        GameHandler.instance.OnPlayerRestart += Instance_OnPlayerRestart;
        PlayerManager.littlePlayerInstance.GetComponent<PlayerHealth>().OnPlayerDeath += Instance_OnPlayerDeath;
        ResetWaves();
    }

    private void Instance_OnPlayerRestart(object sender, System.EventArgs e)
    {
        ResetWaves();
    }
    private void Instance_OnPlayerDeath(object sender, System.EventArgs e)
    {
        ClearWaves();
    }
    private void Instance_OnPlayerSizeChange(object sender, GameHandler.OnPlayerSizeChangeArgs e)
    {
        if (e.little)
        {
            foreach (EnemyWave wave in waves)
            {
                wave.ResumeWave();
            }
        }
        else
        {
            foreach (EnemyWave wave in waves)
            {
                wave.PauseWave();
            }
        }

    }

    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        ClearWaves();
        //ResetWaves();
    }

    private void ClearWaves()
    {
        print(name);
        foreach (EnemyWave wave in spawnedWaves)
        {
            Destroy(wave.gameObject);
        }
        wavesCompleted = 0;
        spawnedWaves.Clear();
    }
    private void ResetWaves()
    {
        if (arenaIsComplete)
        {
            return;
        }
        wavesCompleted = 0;
        foreach(EnemyWave wave in waves)
        {
            EnemyWave newWave = Instantiate(wave, transform);
            spawnedWaves.Add(newWave);
            newWave.OnWaveComplete += NewWave_OnWaveComplete;
        }
    }

    private void NewWave_OnWaveComplete(object sender, System.EventArgs e)
    {
        print("Boo");
        wavesCompleted += 1;
        if(wavesCompleted == waves.Length)
        {
            arenaIsComplete = true;
        }
    }
}