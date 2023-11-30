using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    [Header("Move Info")]
    [SerializeField] protected Rigidbody rb;

    [SerializeField] private Animator anim;
    [SerializeField] protected float moveSpeed;
    protected string currentAnimName = CacheString.ANIM_IDLE;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;

    [Header("Collier Info")]
    protected Character mainTarget;
    protected List<Character> listTarget = new List<Character>();

    protected Vector3 direc;

    [Header("Weapon Info")]
    [SerializeField] protected WeaponType currentWeaponType;
    [SerializeField] protected Transform holdWeapon;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected float timeDestroy;

    protected bool canAttack;

    protected WeaponData weaponData;
    protected Bullet bulletOjb;

    public Animator Anim { get => anim; set => anim = value; }


    private void Awake()
    {
        OnInit();
        SpawnWeapon();
        GameManager.Instance.ChangeStage(GameState.MainMenu);
    }

    private void SpawnWeapon()
    {
        Instantiate(weaponData.weapon, holdWeapon);
    }

    public void RemoveTargetWhenHit(Character attacker)
    {
        listTarget.Remove(attacker);

    }


    public void Attack()
    {
        isAttack = true;
        transform.LookAt(mainTarget.transform.position);
        ChangeAnim(CacheString.ANIM_ATTACK);
        Invoke(nameof(SpawnBullet), 0.4f);


    }

    //public bool IsShoot()
    //{
    //    bool isShoot = false;

    //    return isShoot;
    //}

    public void SpawnBullet()
    {
        direc = mainTarget.transform.position - transform.position;
        Bullet spawnBullet = LeanPool.Spawn(weaponData.bullet, firePos.position, firePos.rotation);
        bulletOjb = spawnBullet.GetComponent<Bullet>();
        bulletOjb.SeekAttacker(this);
        bulletOjb.SeekDirec(direc);
        holdWeapon.gameObject.SetActive(false);
        bulletOjb.OnDespawn(timeDestroy);
        Invoke(nameof(ResetAttack), 0.4f);
    }

    public void ResetAttack()
    {
        isAttack = false;
        holdWeapon.gameObject.SetActive(true);
    }

    public virtual void OnInit()
    {
        isDead = false;
        isIdle = true;
        isAttack = false;
        //ChangeAnim(CacheString.ANIM_IDLE);
    }

    public virtual void OnDeath()
    {
        isDead = true;
        isIdle = false;


        ChangeAnim(CacheString.ANIM_DEAD);
        Invoke(nameof(OnDespawn), 2f);
    }

    public virtual void OnDespawn()
    {

    }

    public void FindClosestTarget(Vector3 playerPosition, List<Character> listTarget)
    {
        float closestDistance = Mathf.Infinity;
        if (listTarget.Count > 0)
        {
            foreach (Character target in listTarget)
            {
                float distance = Vector3.Distance(playerPosition, target.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    mainTarget = target;
                }
            }
        }
        else
        {
            mainTarget = null;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        //Add target on list when target enter range
        if (other.CompareTag(CacheString.BOT_TAG) || other.CompareTag(CacheString.PLAYER_TAG))
        {
            Character target = other.GetComponent<Character>();
            if (target != this && !target.isDead)
            {
                listTarget.Add(target);
                //Take firt enemy you collect to main target
                //If enmy in range > 1, add enemy to list target
                //if(this.mainTarget == null)
                //{
                //    this.mainTarget = target;
                //    //Debug.Log("Find target");
                //}
                //else
                //{
                //    listTarget.Add(target);
                //    //Debug.Log("Add bot to list");
                //}
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Remove target form list when target exit range
        if (other.gameObject.layer == LayerMask.NameToLayer(CacheString.CHARACTER_LAYER))
        {
            Character targetout = other.GetComponent<Character>();
            //listTarget.Remove(targetout);
            //Enemy exit range will be remove form list or main target
            if (mainTarget == targetout)
            {
                if (listTarget.Count > 0)
                {
                    mainTarget = listTarget[0];
                    listTarget.RemoveAt(0);
                }
                else
                {
                    mainTarget = null;
                }
            }
            else
            {
                listTarget.Remove(targetout);
            }
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, circleRadius);
    //}

}







