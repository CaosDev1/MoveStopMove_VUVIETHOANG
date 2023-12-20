using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public NavMeshAgent agent;
    public float wanderRadius = 6f;
    [SerializeField] protected GameObject targetIcon;
    public IState currentState;
    private Vector3 newPos;

    //private string botName = null;
    public bool isEndPoint => Vector3.Distance(transform.position, newPos) < 1.1f;

    public override void Start()
    {
        base.Start();
        currentState = new PatrolState();
    }
    public override void Update()
    {
        base.Update();
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
        //Debug.Log($"Character Pos: {transform.position}");
    }

    public override void OnInit()
    {
        base.OnInit();
        
        currentAnimName = CacheString.ANIM_IDLE;
        SetWeaponEnemy();
        SetHatSkinEnemy();
        SetPantSkinEnemy();
        ChangeState(new PatrolState());
    }

    private void SetNameEnemy(string characterName)
    {
        if (characterName == null)
        {
            List<NameData> nameDatas = DataManager.Instance.nameDataSO.listName;
            //TO DO: How to get name enemy?
        }
    }

    private void SetWeaponEnemy()
    {
        List<WeaponData> weapons = DataManager.Instance.weaponDataSO.listWeaponData;
        int index = Random.Range(0, weapons.Count - 1);
        CurrentWeaponType = (WeaponType)index;
        if (WeaponData == null)
        {
            WeaponData = DataManager.Instance.GetWeaponData(CurrentWeaponType);
        }
    }

    private void SetHatSkinEnemy()
    {
        List<HatData> hatDatas = DataManager.Instance.hatDataSO.hatDataList;
        int index = Random.Range(0,hatDatas.Count - 1);
        PlayerHatType = (HatItemType)index;
        if (PlayerHatData == null)
        {
            PlayerHatData = DataManager.Instance.GetHatData(PlayerHatType);
        }
    }

    private void SetPantSkinEnemy()
    {
        List<PantData> pantDatas = DataManager.Instance.pantDataSO.pantDataList;
        int index = Random.Range(0,pantDatas.Count - 1);
        PlayerPantType= (PantItemType)index;
        if(PlayerPantData == null)
        {
            PlayerPantData = DataManager.Instance.GetPantData(PlayerPantType);
        }
    }

    public void TurnOnTargetIcon()
    {
        targetIcon.SetActive(true);
    }

    public void TurnOffTargetIcon()
    {
        targetIcon.SetActive(false);
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
