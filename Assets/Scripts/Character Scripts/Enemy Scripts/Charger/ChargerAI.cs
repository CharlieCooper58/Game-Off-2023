using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerAI : EnemyAI
{
    public float attackRange;
    [SerializeField] int contactDamage;
    [SerializeField] float jumpTime;
    [SerializeField] Transform meleeDamagePoint;
    [SerializeField] float meleeDamageRadius;
    Rigidbody rb;
    //[SerializeField] BoxCollide


    [Header("Grounded Check")]
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] Transform groundCheckPoint;
    public override void Initialize()
    {
        base.Initialize();
        rb = GetComponent<Rigidbody>();
        contactDamage = 0;
    }
    public override void MakeStateDecisions()
    {
        switch (state)
        {
            case AIState.move:
                TickMovementTimer();
                if(Vector3.SqrMagnitude(target.transform.position-transform.position) < attackRange * attackRange)
                {
                    state = AIState.attack;
                    navMeshAgent.enabled = false;
                    contactDamage = attackDamage;
                    Vector3 newVelocity = CalculateJumpVelocity();
                    rb.velocity = newVelocity;
                }
                break;
            case AIState.attack:
                if(rb.velocity.y <= 0 && Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundCheckMask))
                {
                    navMeshAgent.enabled=true;
                    state = AIState.move;
                    contactDamage = 0;
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

    private Vector3 CalculateJumpVelocity()
    {
        // Calculate the displacement in XZ plane
        Vector3 playerPositionXZ = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
        Vector3 enemyPositionXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        float displacementXZ = Vector3.Distance(playerPositionXZ, enemyPositionXZ);

        // Calculate the required horizontal velocity
        float horizontalVelocity = displacementXZ / jumpTime;

        // Calculate the vertical velocity to reach the desired height
        float verticalVelocity = (1.4f + 0.5f * GameHandler.instance.gravity * Mathf.Pow(jumpTime, 2)) / jumpTime;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
