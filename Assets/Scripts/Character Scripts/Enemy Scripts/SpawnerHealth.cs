using UnityEngine;
using System;
public class SpawnerHealth : CharacterHealth
{
    EnemyManager enemyManager;

    public class SpawnerDeathEventArgs: EventArgs
    {
        public Spawner deadSpawner;
    }
    public event EventHandler<SpawnerDeathEventArgs> OnSpawnerDeath;

    public override void Initialize() {
        base.Initialize();
        enemyManager = GetComponent<EnemyManager>();
    }
    public override void TakeDamage(int damage, bool brutal = false) {
        //Spawner does not take damage, unless it is melee.
    }
    public void TakeMeleeAttack(Vector3 direction, float force, int damage) {
        base.TakeDamage(damage, true);//I hijacked the overridden method here. Obviously a bad practice.
    }
    protected override void Die(bool brutal = false)
    {
        OnSpawnerDeath.Invoke(this, new SpawnerDeathEventArgs { deadSpawner = GetComponent<Spawner>() });
        base.Die(brutal);
    }
}