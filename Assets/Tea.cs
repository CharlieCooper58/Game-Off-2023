using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Tea : Interactable
{
    [SerializeField] EventReference teaSipSound;
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        AudioManager.instance.Play(teaSipSound);
        PlayerManager.bigPlayerInstance?.characterHealth.Heal();
        if (GameHandler.instance.Victory)
        {
            GameHandler.instance.EndGame();
        }
    }
}
