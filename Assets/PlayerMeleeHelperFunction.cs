using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeHelperFunction : MonoBehaviour
{
    PlayerAttacker playerAttacker;

    private void Awake()
    {
        playerAttacker = GetComponentInParent<PlayerAttacker>();
    }

    public void Attack()
    {
        if (playerAttacker != null)
        {
            playerAttacker.MeleeAttack();
        }
    }
}
