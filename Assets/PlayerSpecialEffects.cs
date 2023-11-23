using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialEffects : MonoBehaviour
{
    [SerializeField] private EventReference PlayerDeath;
    [SerializeField] private EventReference PlayerHurt;
    [SerializeField] Organs[] organs;
    [SerializeField] SplatterEffect splatter;


    PlayerManager manager;
    public void Initialize() {
        manager = GetComponent<PlayerManager>();
        manager.characterHealth.OnCharacterDeath += CharacterHealth_OnCharacterDeath;
        manager.characterHealth.OnCharacterHit += CharacterHealth_OnCharacterHit;
    }

    private void CharacterHealth_OnCharacterDeath(object sender, CharacterHealth.CharacterDeathEventArgs e) {
        throw new System.NotImplementedException();
    }

    private void CharacterHealth_OnCharacterHit(object sender, System.EventArgs e) {
        AudioManager.instance.PlayOneShot(PlayerHurt, this.transform.position);
    }
}
