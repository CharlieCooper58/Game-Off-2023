using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class Tooltip : MonoBehaviour
{
    public string tooltipText;

    public bool objective;

    [SerializeField] EventReference tooltipSound;
    private void OnTriggerEnter(Collider other)
    {
        PlayerUIController uiController = other.GetComponent<PlayerUIController>();
        if (uiController != null)
        {
            AudioManager.instance.Play(tooltipSound);
            if (objective)
            {
                uiController.SetObjectiveText(tooltipText);
                Destroy(gameObject);
            }
            else
            {
                uiController.ShowTooltip(tooltipText);
                Destroy(gameObject);
            }

        }
    }
}
