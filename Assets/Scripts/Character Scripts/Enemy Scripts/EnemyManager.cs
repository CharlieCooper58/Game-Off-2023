using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    // Start is called before the first frame update
    public EnemyAI enemyAI;
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
        PlayerManager.littlePlayerInstance.GetComponent<PlayerHealth>().OnPlayerDeath += Instance_OnPlayerDeath;
    }
    private void OnDestroy()
    {
        PlayerManager.littlePlayerInstance.GetComponent<PlayerHealth>().OnPlayerDeath -= Instance_OnPlayerDeath;
    }
    private void Instance_OnPlayerDeath(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        enemyAI.MakeStateDecisions();
    }
    public void DisposeOfSelf()
    {
        Destroy(gameObject);
    }
}
