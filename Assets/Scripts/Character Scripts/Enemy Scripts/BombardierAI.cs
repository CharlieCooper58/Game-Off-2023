using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyAI;
using UnityEngine.AI;

public class BombardierAI : EnemyAI
{
    MortarWeapon mortarWeapon;
    float playerDistance;
    Vector3 playerDirection;
    float thresholdDistance = 15f;
    bool isAttacking;
    public override void Initialize() {
        base.Initialize();
        mortarWeapon = GetComponent<MortarWeapon>();
        mortarWeapon.weaponDamage = attackDamage;
    }
    public override void MakeStateDecisions() {
        playerDirection = target.transform.position - mortarWeapon.muzzleEndpoint.position;
        playerDistance = Vector3.Magnitude(playerDirection);

        base.MakeStateDecisions();
    }
    protected override void OnAIStateMove() {
        base.OnAIStateMove();
        if (playerDistance<thresholdDistance) {
            navMeshAgent.SetDestination(transform.position);
            state = AIState.attack;
        } else {
            TickMovementTimer();
        }
    }
    protected override void OnAIStateAttack() {
        base.OnAIStateAttack();
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(playerDirection.x, 0, playerDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        if (isAttacking) {
            return;
        } else if (playerDistance < thresholdDistance) {
            if (enemyManager.animationHandler.PlayTargetAnimation("SpitterAttack", 1)) {
                isAttacking = true;
            }
        } else {
            state = AIState.move;
        }
    }

    public void Attack() {
        mortarWeapon.OnTriggerPressed(target.transform.position);
        isAttacking = false;
    }

}
