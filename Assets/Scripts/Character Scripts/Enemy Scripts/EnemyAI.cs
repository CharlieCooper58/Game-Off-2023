using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    protected EnemyManager enemyManager;
    NavMeshAgent navMeshAgent;
    NavMeshPath pathToDestination;
    protected PlayerManager target;
    Vector3 nextTargetLocation;

    float navigationTimer;
    const float navigationTimerMax = 0.1f;

    [SerializeField] protected LayerMask playerLayer;

    [SerializeField] protected int attackDamage;

    public virtual void Initialize()
    {
        enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameHandler.instance.playerManager;
        state = AIState.move;

        //playerLayer = LayerMask.NameToLayer("Player");
    }
    protected enum AIState
    {
        idle,
        move,
        attack
    }

    protected AIState state;


    public virtual void MakeStateDecisions()
    {

        switch (state)
        {
            case AIState.move:
                TickMovementTimer();
                break;
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
}
