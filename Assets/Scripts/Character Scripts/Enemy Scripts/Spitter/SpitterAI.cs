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
        base.MakeStateDecisions();
    }
    protected override void OnAIStateMove()
    {
        base.OnAIStateMove();
        if (SightCast())
        {
            navMeshAgent.SetDestination(transform.position);
            state = AIState.attack;
        }
        else
        {
            TickMovementTimer();
        }
    }
    protected override void OnAIStateAttack()
    {
        base.OnAIStateAttack();
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        if (isAttacking)
        {
            return;
        }
        else if (SightCast())
        {
            if (enemyManager.animationHandler.PlayTargetAnimation("SpitterAttack", 1))
            {
                isAttacking = true;
            }
        }
        else
        {
            state = AIState.move;
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
