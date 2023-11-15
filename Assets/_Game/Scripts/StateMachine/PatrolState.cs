using System.Numerics;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private float timer = 0f;
    private float wanderTimer = 0.5f;
    
    public void OnEnter(Enemy enemy)
    {
        
    }
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            enemy.SetDirection();
            timer = 0;
        }

    }

    public void OnExit(Enemy enemy)
    {

    }

    
}
