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
        //Enemy random move
        if(timer >= wanderTimer && !enemy.isAttack)
        {
            enemy.SetDirection();
            timer = 0f;
            wanderTimer = Random.Range(3f, 5f);
        }
        
        if (!enemy.isEndPoint)
        {
            enemy.isIdle = false;
            enemy.isAttack = false;
            enemy.ChangeAnim(CacheString.ANIM_RUN);
        }
        else if(!enemy.isAttack)
        {
            enemy.isIdle = true;
            enemy.ChangeAnim(CacheString.ANIM_IDLE);
        }else if(enemy.MainTarget != null)
        {
            enemy.ChangeState(new AttackState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }

    
}
