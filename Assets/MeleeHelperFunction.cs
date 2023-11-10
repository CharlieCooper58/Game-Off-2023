using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHelperFunction : MonoBehaviour
{
    ChargerAI charger;
    private void Start()
    {
        charger = GetComponentInParent<ChargerAI>();
    }

    public void ChargerAttack()
    {
        charger.CheckDamage();
    }
}
