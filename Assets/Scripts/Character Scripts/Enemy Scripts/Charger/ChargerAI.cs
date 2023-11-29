using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : EnemyAI
{
    public float attackRange;
    [SerializeField] int contactDamage;
    [SerializeField] float jumpTime;
    [SerializeField] float jumpCooldown;


    public override void Initialize()
    {
        base.Initialize();
        contactDamage = 0;

    }
    protected override void OnAIStateMove()
    {
        base.OnAIStateMove();
        TickMovementTimer();
        if (Vector3.SqrMagnitude(target.transform.position - transform.position) < attackRange * attackRange)
        {
            state = AIState.attack;
            EnableRigidbody();
            contactDamage = attackDamage;
            Vector3 newVelocity = CalculateJumpVelocity();
            enemyManager.animationHandler.PlayTargetAnimation("Attack", 0);
            rb.velocity = newVelocity;
            AudioManager.instance.Play(attackSound, transform.position);
        }
    }
    protected override void OnAIStateAttack()
    {
        base.OnAIStateAttack();
        if (CheckRigidbodyShouldBeInactive())
        {
            //DisableRigidbody();
            SetAIStateStun(jumpCooldown, AIState.move);
            contactDamage = 0;
        }
    }
    private Vector3 CalculateJumpVelocity()
    {
        // Calculate the displacement in XZ plane
        Vector3 playerPositionXZ = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
        Vector3 enemyPositionXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        float displacementXZ = Vector3.Distance(playerPositionXZ, enemyPositionXZ);

        // Calculate the required horizontal velocity
        float horizontalVelocity = displacementXZ / jumpTime;

        // Calculate the vertical velocity to reach the desired height
        float verticalVelocity = ((target.transform.position.y+1.2f-transform.position.y) + 0.5f * GameHandler.instance.gravity * Mathf.Pow(jumpTime, 2)) / jumpTime;

        // Apply the calculated velocity
        Vector3 jumpVelocity = horizontalVelocity*(playerPositionXZ - enemyPositionXZ).normalized+Vector3.up*verticalVelocity;
        return jumpVelocity;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(contactDamage > 0 && collision.gameObject == target.gameObject)
        {
            target.GetComponent<CharacterHealth>().TakeDamage(contactDamage);
            contactDamage = 0;
        }
    }


}
