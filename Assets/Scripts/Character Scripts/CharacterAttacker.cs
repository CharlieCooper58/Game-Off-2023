using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttacker : MonoBehaviour
{
    [SerializeField] protected Weapon activeWeapon;

    public virtual bool OnWeaponUsed()
    {
        return activeWeapon.OnTriggerPressed();
    }
    public virtual void OnWeaponReleased()
    {
        activeWeapon.OnTriggerReleased();
    }
}
