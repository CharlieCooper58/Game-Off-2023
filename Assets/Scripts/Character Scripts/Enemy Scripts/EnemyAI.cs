using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    protected EnemyManager enemyManager;
    protected NavMeshAgent navMeshAgent;
    protected AgentLinkMover navMeshLinkMover;
    protected NavMeshPath path;

    protected GameObject target;
    public Plant plantTarget;
    protected PlayerManager playerTarget;

    [Header("Grounded Check")]
    [SerializeField] float groundCheckRadius;
    Vector3 groundCheckBounds;
    [SerializeField] LayerMask groundCheckMask;
    [SerializeField] Transform groundCheckPoint;

    float navigationTimer;
    const float navigationTimerMax = 0.1f;

    protected Rigidbody rb;
    [SerializeField] protected LayerMask playerLayer;

    [SerializeField] float stunTimerMax;
    protected float stunTimer;
    bool nearDeathStunned = false;
    bool beenNearDeath = false;

    [SerializeField] protected int attackDamage;

    public virtual void Initialize()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshLinkMover = GetComponent<AgentLinkMover>();
        navMeshAgent.updateRotation = false;
        plantTarget = FindObjectOfType<Plant>();
        playerTarget = PlayerManager.littlePlayerInstance;
        if (target == null) target = plantTarget.gameObject;
        stunTimer = 0.5f;
        prevState = AIState.move;
        state = AIState.stun;
        groundCheckBounds = GetComponent<BoxCollider>().bounds.extents;
        groundCheckBounds.y = groundCheckRadius;
        rb = GetComponent<Rigidbody>();
        EnableRigidbody();

        //playerLayer = LayerMask.NameToLayer("Player");
    }
    public enum AIState
    {
        idle,
        move,
        attack,
        stun
    }

    public AIState state;
    protected AIState prevState;

    public void TargetPlayer()
    {
        target = GameHandler.instance.littlePlayerManager.gameObject;
    }
    public virtual void MakeStateDecisions()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (navMeshAgent.enabled && navMeshAgent.velocity != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(navMeshAgent.velocity, Vector3.up), Vector3.up);
        else if(rb.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(rb.velocity, Vector3.up), Vector3.up);
        }
        else if(transform.up != Vector3.up)
        {
            transform.rotation = Quaternion.LookRotation(transform.forward, Vector3.up);
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
    protected virtual void OnAIStateStun()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        if (CheckRigidbodyShouldBeInactive())
        {
            DisableRigidbody();
            if(prevState == AIState.move)
            {
                SetAIStateMove();
            }
            else
            {
                SetAIStateAttack();
            }
            if (nearDeathStunned)
            {
                nearDeathStunned = false;
                enemyManager.animationHandler.SetAnimationTrigger("StunTriggerStop");
            }
        }
    }
    protected virtual void SetAIStateMove()
    {
        enemyManager.animationHandler.PlayTargetAnimation("Move", 0);
        state = AIState.move;
    }
    protected virtual void SetAIStateAttack()
    {
        state = AIState.attack;
    }
    protected virtual void SetAIStateStun(float stunTime, AIState prevState)
    {
        this.prevState = prevState;
        this.stunTimer = stunTime;
        state = AIState.stun;
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
        return rb.velocity.y <= 0 && Physics.CheckBox(groundCheckPoint.position, groundCheckBounds, Quaternion.identity, groundCheckMask) && stunTimer <= 0;
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
        if (state != AIState.stun)
        {
            prevState = state;
        }
        state = AIState.stun;
        SetAIStateStun(Mathf.Max(stunTimer, 0.3f), (state!=AIState.stun)?state:prevState);
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
        nearDeathStunned = true;
        SetAIStateStun(Mathf.Max(stunTimer, stunTimerMax), (state != AIState.stun) ? state : prevState);
    }

    public void EnableRigidbody()
    {
        navMeshAgent.enabled = false;
        navMeshLinkMover.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    public void DisableRigidbody()
    {
        navMeshAgent.enabled = true;
        navMeshLinkMover.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckBounds);
    }
}