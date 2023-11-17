using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [Header("Navmesh Info")]
    public NavMeshAgent agent;
    private IState currentState;
    public float wanderRadius = 6f;
    public Vector3 newPos;
    public bool isTarget => Vector3.Distance(transform.position, newPos) < 0.1f;

    public override void Start()
    {
        base.Start();
        ChangeState(new PatrolState());
    }
    public override void Update()
    {
        base.Update();
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }

    public void SetDirection()
    {
        newPos = RandomNavSphere(transform.position, wanderRadius, -1);
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
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }


}
