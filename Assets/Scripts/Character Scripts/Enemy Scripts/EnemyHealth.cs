using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    EnemyManager enemyManager;
    public override void Initialize()
    {
        base.Initialize();
        enemyManager = GetComponent<EnemyManager>();
    }
    public override void TakeDamage(int damage, bool brutal = false)
    {
        base.TakeDamage(damage, brutal);
        if(currentHP < maxHP / 5)
        {
            enemyManager.enemyAI.SetLowHealthStun();
        }
    }
}
