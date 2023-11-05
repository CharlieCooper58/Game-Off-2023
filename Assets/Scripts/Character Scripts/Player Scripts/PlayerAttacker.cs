using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttacker : CharacterAttacker
{
    public Image crossHair;
    public override bool OnWeaponUsed()
    {
        if (base.OnWeaponUsed())
        {
            StartCoroutine("ShrinkCrosshair");
            return true;
        }
        return false;
    }

    IEnumerator ShrinkCrosshair()
    {
        Vector3 crosshairSize = new Vector3(0.35f, 0.35f, 1);
        crossHair.rectTransform.localScale = crosshairSize;
        float elapsedTime = 0.0f;
        while (elapsedTime < 0.3f)
        {
            crosshairSize = Vector3.one*(0.35f+2*elapsedTime);

            crossHair.rectTransform.localScale = crosshairSize;
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        crossHair.rectTransform.localScale = Vector3.one;
    }
}
