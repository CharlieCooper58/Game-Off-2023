using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    protected EnemyManager enemyManager;
    protected NavMeshAgent navMeshAgent;
    protected PlayerManager target;

    [Header("Grounded Check")]
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] Transform groundCheckPoint;

    float navigationTimer;
    const float navigationTimerMax = 0.1f;

    protected Rigidbody rb;
    [SerializeField] protected LayerMask playerLayer;

    [SerializeField] float stunTimerMax;
    float stunTimer;
    bool nearDeathStunned = false;
    bool beenNearDeath = false;

    [SerializeField] protected int attackDamage;

    public virtual void Initialize()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = PlayerManager.littlePlayerInstance;
        state = AIState.move;

        rb = GetComponent<Rigidbody>();
        DisableRigidbody();

        //playerLayer = LayerMask.NameToLayer("Player");
    }
    protected enum AIState
    {
        idle,
        move,
        attack,
        stun
    }

    protected AIState state;
    AIState prevState;


    public virtual void MakeStateDecisions()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        switch (state)
        {
            case AIState.move:
                OnAIStateMove();
                break;
            case AIState.attack:
                OnAIStateAttack();
                break;
            case AIState.stun:
                OnAIStateStun();
                break;
        }
    }

    protected virtual void OnAIStateMove() { }
    protected virtual void OnAIStateAttack() { }
    protected virtual void OnAIStateStun() {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        if (CheckRigidbodyShouldBeInactive())
        {
            DisableRigidbody();
            state = prevState;
            if (nearDeathStunned)
            {
                nearDeathStunned = false;
                enemyManager.animationHandler.SetAnimationTrigger("StunTriggerStop");
            }
        }
    }

    protected void TickMovementTimer()
    {
        navigationTimer -= Time.deltaTime;

        // Check if it's time to set a new destination
        if (navigationTimer <= 0f)
        {
            navMeshAgent.SetDestination(target.transform.position);
            navigationTimer = navigationTimerMax; // Reset the timer
        }
    }
    protected bool CheckRigidbodyShouldBeInactive()
    {
        if (rb.isKinematic)
        {
            return true;
        }
        return rb.velocity.y <= 0 && Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundCheckMask) && stunTimer <= 0;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
    public void TakeMeleeAttack(Vector3 direction, float force, int damage)
    {
        EnableRigidbody();
        rb.velocity = force * direction.normalized;
        if (nearDeathStunned)
        {
            enemyManager.characterHealth.TakeDamage(1000, true);
        }
        else
        {
            enemyManager.characterHealth.TakeDamage(damage);
        }
        stunTimer = Mathf.Max(stunTimer, .3f);
        if(state != AIState.stun)
        {
            prevState = state;
        }
        state = AIState.stun;
    }

    public void SetLowHealthStun()
    {
        if (beenNearDeath)
        {
            return;
        }
        beenNearDeath = true;
        EnableRigidbody();
        enemyManager.animationHandler.SetAnimationTrigger("StunTrigger");
        stunTimer = Mathf.Max(stunTimer, stunTimerMax);
        if (state != AIState.stun)
        {
            prevState = state;
        }
        nearDeathStunned = true;
        state = AIState.stun;
    }

    public void EnableRigidbody()
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    public void DisableRigidbody()
    {
        navMeshAgent.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }
}
