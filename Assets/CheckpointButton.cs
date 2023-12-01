using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class CheckpointButton : Interactable
{
    Checkpoint m_checkPoint;
    bool beenPressed = false;

    public string saveText;
    public string growText;
    public string incompleteText;
    [SerializeField] EventReference buttonPressSound;
    [SerializeField] EventReference checkpointSound;
    [SerializeField] EventReference growSound;

    private void Awake()
    {
        m_checkPoint = GetComponentInParent<Checkpoint>();
        if (m_checkPoint.isFirstCheckpoint)
        {
            highlightText = growText;
            beenPressed = true;
        }
        else
        {
            highlightText = (m_checkPoint.myArena != null && !m_checkPoint.myArena.arenaIsComplete) ? incompleteText : saveText;
        }
    }
    public override void OnHoverEnter()
    {
        highlightText = m_checkPoint.isFirstCheckpoint ? growText : ((m_checkPoint.myArena != null && !m_checkPoint.myArena.arenaIsComplete) ? incompleteText : saveText);
        base.OnHoverEnter();
    }
    public override void OnInteract(PlayerInteractor interactor)
    {
        base.OnInteract(interactor);
        AudioManager.instance.Play(buttonPressSound);

        if (!beenPressed)
        {
            beenPressed = m_checkPoint.TrySetCheckpoint();
            if (beenPressed)
            {
                AudioManager.instance.Play(checkpointSound);
                highlightText = growText;
            }
        }
        else
        {
            AudioManager.instance.Play(growSound);
            GameHandler.instance.SetActivePlayer(little: false, true);
            if (m_checkPoint.isLastCheckpoint)
            {
                GameHandler.instance.SetVictory();
                PlayerManager.bigPlayerInstance.playerUIController.SetObjectiveText("Celebrate your victory with a nice cup of tea");
            }
            else
            {
                PlayerManager.bigPlayerInstance.playerUIController.SetObjectiveText("Feeling low? Drink some tea to get your health up!");
            }
        }
    }
}
