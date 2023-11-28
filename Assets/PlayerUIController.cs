using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerUIController : MonoBehaviour
{
    public TMP_Text tooltipText;
    GameObject textPanel;
    private void Awake()
    {
        tooltipText.gameObject.SetActive(false);
    }
    IEnumerator FlashText(string text)
    {
        tooltipText.gameObject.SetActive(true);
        tooltipText.text = text;
        yield return new WaitForSeconds(5);
        if(tooltipText.text == text)
        {
            tooltipText.gameObject.SetActive(false);
        }
    }
    public void SetCurrentText(string text)
    {
        tooltipText.gameObject.SetActive(true);
        tooltipText.text=text;
    }
    public void StopShowingText()
    {
        tooltipText.gameObject.SetActive(false);
        tooltipText.text = "";
    }
    public void ShowTooltip(string text)
    {
        StartCoroutine("FlashText", text);
    }
}
