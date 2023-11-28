using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public string tooltipText;

    private void OnTriggerEnter(Collider other)
    {
        PlayerUIController uiController = other.GetComponent<PlayerUIController>();
        if (uiController != null)
        {
            uiController.ShowTooltip(tooltipText);
            Destroy(gameObject);
        }
    }
}
