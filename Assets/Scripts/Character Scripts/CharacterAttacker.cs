using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] Weapon activeWeapon;

    public virtual bool OnWeaponUsed()
    {
        return activeWeapon.OnTriggerPressed();
    }
    public void OnWeaponReleased()
    {
        activeWeapon.OnTriggerReleased();
    }
}
