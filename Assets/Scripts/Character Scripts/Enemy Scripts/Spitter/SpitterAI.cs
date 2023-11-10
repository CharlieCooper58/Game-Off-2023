using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterAI : EnemyAI
{
    ProjectileWeapon spitPoint;
    Vector3 playerDirection;
    bool isAttacking;
    public override void Initialize()
    {
        base.Initialize();
        spitPoint = GetComponent<ProjectileWeapon>();
        spitPoint.weaponDamage = attackDamage;
    }
    public override void MakeStateDecisions()
    {
        playerDirection = target.transform.position - spitPoint.muzzleEndpoint.position;
        switch (state)
        {
            case AIState.move:
                if (SightCast())
                {
                    navMeshAgent.SetDestination(transform.position);
                    state = AIState.attack;
                }
                else
                {
                    TickMovementTimer();
                }
                break;
            case AIState.attack:
                if (isAttacking)
                {
                    break;
                }
                else if (SightCast())
                {
                    if(enemyManager.animationHandler.PlayTargetAnimation("SpitterAttack", 1))
                    {
                        isAttacking = true;
                    }
                }
                else
                {
                    state = AIState.move;
                }
                break;
        }
    }

    public void Attack()
    {
        spitPoint.OnTriggerPressed(playerDirection);
        isAttacking = false;
    }

    private bool SightCast()
    {
        RaycastHit hit;
        if(Physics.Raycast(spitPoint.muzzleEndpoint.position, playerDirection, out hit, playerLayer))
        {
            return hit.collider.gameObject == target.gameObject;
        }
        return false;
    }
}
