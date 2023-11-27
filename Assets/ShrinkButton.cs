using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkButton : Interactable
{
    public override void OnInteract()
    {
        base.OnInteract();
        GameHandler.instance.SetActivePlayer(!GameHandler.instance.playerIsLittle);
    }
}
