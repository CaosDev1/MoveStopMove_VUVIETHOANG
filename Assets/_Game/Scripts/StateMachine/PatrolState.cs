using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private float timer = 0f;
    private float wanderTimer = 5f;

    public void OnEnter(Enemy enemy)
    {
        enemy.SetDirection();
    }
    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        
        if(timer >= wanderTimer && !enemy.isAttack)
        {
            enemy.SetDirection();
            timer = 0f;
            wanderTimer = Random.Range(3f, 5f);
        }

        if (!enemy.isTarget)
        {
            enemy.isIdle = false;
            enemy.anim.SetBool(ConstString.IS_IDLE_STRING, false);
        }
        else
        {
            enemy.isIdle = true;
            enemy.anim.SetBool(ConstString.IS_IDLE_STRING, true);
        }
    }

    public void OnExit(Enemy enemy)
    {

    }

    
}
