using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointButton : Interactable
{
    Checkpoint m_checkPoint;
    bool beenPressed = false;

    public string saveText;
    public string growText;
    public string incompleteText;
    private void Awake()
    {
        m_checkPoint = GetComponentInParent<Checkpoint>();
        highlightText = m_checkPoint.isFirstCheckpoint?growText:(m_checkPoint.myArena.arenaIsComplete?incompleteText:saveText);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (!beenPressed)
        {
            beenPressed = m_checkPoint.TrySetCheckpoint();
            if (beenPressed)
            {
                highlightText = growText;
            }
        }
        else
        {
            GameHandler.instance.SetActivePlayer(little: false, true);
        }
    }
}
