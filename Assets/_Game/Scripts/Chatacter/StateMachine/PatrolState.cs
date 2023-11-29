using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine;

public class PatrolState : IState
{
    private float timer = 0f;
    private float wanderTimer = 5f;

    public void OnEnter(Enemy enemy)
    {
        enemy.SetDirection();
        enemy.isAttack = false;
        enemy.isIdle = false;
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

        if (!enemy.isEndPoint)
        {
            enemy.isIdle = false;
            enemy.ChangeAnim(CacheString.ANIM_RUN);
        }
        else if(!enemy.isAttack)
        {
            enemy.isIdle = true;
            enemy.ChangeAnim(CacheString.ANIM_IDLE);
        }
    }

    public void OnExit(Enemy enemy)
    {

    }

    
}
