using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("Navmesh Info")]
    public NavMeshAgent agent;
    private IState currentState;
    private float wanderRadius = 10f;

    public override void Start()
    {
        base.Start();
        ChangeState(new PatrolState());
    }
    public override void Update()
    {
        base.Update();
        if(currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    public void SetDirection()
    {
        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
        agent.SetDestination(newPos);
    }
    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    

    public void ChangeState(IState newState)
    {
        if(currentState != null) 
        {
            currentState.OnExit(this);
        }

        currentState = newState;
        
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    
}
