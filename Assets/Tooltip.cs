using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string tooltipText;

    public bool objective;

    private void OnTriggerEnter(Collider other)
    {
        PlayerUIController uiController = other.GetComponent<PlayerUIController>();
        if (uiController != null)
        {
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
