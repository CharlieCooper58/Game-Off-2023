using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerUIController : MonoBehaviour
{
    public TMP_Text tooltipText;
    public TMP_Text objectiveText;
    public GameObject tooltipPanel;
    public GameObject objectivePanel;
    private void Awake()
    {
        tooltipPanel.gameObject.SetActive(false);
        objectivePanel.gameObject.SetActive(false);
    }
    IEnumerator FlashText(string text)
    {
        tooltipPanel.gameObject.SetActive(true);
        tooltipText.text = text;
        yield return new WaitForSeconds(5);
        if(tooltipText.text == text)
        {
            tooltipPanel.gameObject.SetActive(false);
        }
    }
    public void SetCurrentText(string text)
    {
        tooltipPanel.gameObject.SetActive(true);
        tooltipText.text=text;
    }
    public void StopShowingText()
    {
        tooltipPanel.gameObject.SetActive(false);
        tooltipText.text = "";
    }
    public void ShowTooltip(string text)
    {
        StartCoroutine("FlashText", text);
    }

    public void ClearObjectiveText()
    {
        objectiveText.text = "";
        objectivePanel.gameObject.SetActive(false);
    }
    public void SetObjectiveText(string text)
    {
        objectivePanel.gameObject.SetActive(true);
        objectiveText.text = text;
    }
}
