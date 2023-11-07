using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttacker : CharacterAttacker
{
    public Image crossHair;
    [SerializeField] List<Weapon> weapons;
    int currentWeaponIndex;

    public void Initialize()
    {
        currentWeaponIndex = 0;
        SetWeapon(0);
        for(int i = 1; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }
    public void AcquireWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
        SwitchWeapon(weapons.Count - 1);
    }
    public void CycleWeapons(int direction = 1)
    {
        int newIndex = currentWeaponIndex + direction;

        if (newIndex < 0)
        {
            currentWeaponIndex = weapons.Count - 1;
        }
        else if (newIndex >= weapons.Count)
        {
            currentWeaponIndex = 0;
        }
        else
        {
            currentWeaponIndex = newIndex;
        }

        SetWeapon(currentWeaponIndex);
    }
    public bool SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex >= weapons.Count)
        {
            return false;
        }
        SetWeapon(weaponIndex);
        return true;
    }
    public void SetWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
        {
            return; // Index out of bounds
        }

        if (!activeWeapon || activeWeapon != weapons[weaponIndex])
        {
            // Disable the current active weapon if it's different from the new one
            if (activeWeapon != null)
            {
                activeWeapon.gameObject.SetActive(false);
            }

            // Enable the new weapon
            weapons[weaponIndex].gameObject.SetActive(true);

            // Set the new weapon as the active weapon
            activeWeapon = weapons[weaponIndex];
        }
    }
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
