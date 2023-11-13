using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    // Start is called before the first frame update
    EnemyAI enemyAI;
    EnemySpecialEffects specialEffects;
    protected override void Awake()
    {
        base.Awake();
        enemyAI = GetComponent<EnemyAI>();
        specialEffects = GetComponent<EnemySpecialEffects>();
    }
    protected override void Start()
    {
        base.Start();
        enemyAI.Initialize();
        specialEffects.Initialize();
    }

    void Update()
    {
        enemyAI.MakeStateDecisions();
    }
}
