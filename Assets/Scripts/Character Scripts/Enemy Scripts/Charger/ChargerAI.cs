using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : EnemyAI
{
    public float attackRange;

    [SerializeField] Transform meleeDamagePoint;
    [SerializeField] float meleeDamageRadius;



    public override void MakeStateDecisions()
    {
        switch (state)
        {
            case AIState.move:
                TickMovementTimer();
                if(Vector3.SqrMagnitude(target.transform.position-transform.position) < attackRange * attackRange)
                {
                    enemyManager.animationHandler.PlayTargetAnimation("Attack", 1);
                }
                break;
        }
    }
    public void CheckDamage()
    {
        if(Physics.OverlapSphere(meleeDamagePoint.position, meleeDamageRadius, playerLayer).Length > 0)
        {
            target.characterHealth.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(meleeDamagePoint.position, meleeDamageRadius);
    }
}
