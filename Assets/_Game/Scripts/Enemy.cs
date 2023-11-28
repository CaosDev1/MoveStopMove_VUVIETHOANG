using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    public float wanderRadius = 6f;
    //[SerializeField] protected GameObject indicator;
    private IState currentState;
    private Vector3 newPos;

    public bool isEndPoint => Vector3.Distance(transform.position, newPos) < 0.1f;

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

    public override void OnInit()
    {
        base.OnInit();
        SetWeaponEnemy();
        ChangeState(new PatrolState());
    }

    private void SetWeaponEnemy()
    {
        List<WeaponData> weapons = DataManager.Instance.weaponDataSO.listWeaponData;
        int index = Random.Range(0, weapons.Count - 1);
        currentWeaponType = (WeaponType)index;
        if (weaponData == null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }
    }

    public void TurnOnIndicator()
    {
        //indicator.SetActive(true);
    }

    public void TurnOffIndicator()
    {
        //indicator.SetActive(false);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        LevelManager.Instance.EnemyDeath(this);
    }

    public override void OnDespawn()
    {
        LeanPool.Despawn(this);
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
