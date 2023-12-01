using UnityEngine;

public class AttackState : IState
{
    private float time = 0f;
    private float delayTime = 3f;
    public void OnEnter(Enemy enemy)
    {
        enemy.Attack();
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        
        if (time > delayTime)
        {
            enemy.ChangeState(new PatrolState());
        }

    }

    public void OnExit(Enemy enemy)
    {

    }
}
