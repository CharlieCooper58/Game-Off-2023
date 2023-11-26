using UnityEngine;

public class SpawnerHealth : CharacterHealth
{
    EnemyManager enemyManager;
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
}