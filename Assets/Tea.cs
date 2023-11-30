using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : Interactable
{
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        PlayerManager.bigPlayerInstance?.characterHealth.Heal();
    }
}
