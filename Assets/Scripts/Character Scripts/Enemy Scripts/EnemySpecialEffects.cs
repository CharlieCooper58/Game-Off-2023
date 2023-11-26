using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemySpecialEffects : MonoBehaviour
{
    [SerializeField] private EventReference BugDeath;
    [SerializeField] private EventReference BugHurt;
    [SerializeField] private EventReference BugWalk;
    [SerializeField] Organs[] organs;
    [SerializeField] SplatterEffect splatter;


    CharacterHealth characterHealth;
    EnemyAI ai;
    EventInstance walkSoundEvent;
    

    private void Start() 
    {
        characterHealth = GetComponent<CharacterHealth>();
        characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
        characterHealth.OnCharacterHit += CharacterHealth_OnCharacterHit;

        ai = GetComponent<EnemyAI>();
        walkSoundEvent = AudioManager.instance.Play(BugWalk, this.transform.position);
    }

    private void CharacterHealth_OnCharacterHit(object sender, System.EventArgs e) {
        AudioManager.instance.PlayOneShot(BugHurt, this.transform.position);
    }

    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e)
    {
        walkSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        int numOrgans;
        numOrgans = e.isBrutal? Random.Range(5, 8):0;
        for(int i = 0; i < numOrgans; i++)
        {
            Instantiate(organs[Random.Range(0, organs.Length)], transform.position, Quaternion.identity);
        }
        AudioManager.instance.PlayOneShot(BugDeath, this.transform.position);
        Instantiate(splatter, transform.position, Quaternion.identity);

    }
    private void Update() {
        PLAYBACK_STATE state;
        walkSoundEvent.getPlaybackState(out state);
        if (ai == null) return;
        if (ai.state == EnemyAI.AIState.move && state == PLAYBACK_STATE.STOPPED) { walkSoundEvent.start(); } else if (ai.state != EnemyAI.AIState.move){ walkSoundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


    }
}
