using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class ShrinkButton : Interactable
{
    [SerializeField] EventReference buttonPressSound;
    [SerializeField] EventReference shrinkSound;

    int timesPressed = 0;
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        AudioManager.instance.Play(buttonPressSound);
        AudioManager.instance.Play(shrinkSound);
        PlayerManager.bigPlayerInstance.playerUIController.ClearObjectiveText();
        GameHandler.instance.SetActivePlayer(!GameHandler.instance.playerIsLittle, sendEventCalls:true);
    }
}
