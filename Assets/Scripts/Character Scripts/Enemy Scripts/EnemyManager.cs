using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    // Start is called before the first frame update
    EnemyAI enemyAI;

    protected override void Awake()
    {
        base.Awake();
        enemyAI = GetComponent<EnemyAI>();
    }
    protected override void Start()
    {
        base.Start();
        enemyAI.Initialize();
    }

    void Update()
    {
        enemyAI.MakeStateDecisions();
    }
}
