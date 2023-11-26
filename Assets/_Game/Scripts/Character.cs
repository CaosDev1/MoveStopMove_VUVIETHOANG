using Lean.Pool;
using UnityEngine;
public class Character : MonoBehaviour
{
    [Header("Move Info")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected DynamicJoystick joystick;
    [SerializeField] private Animator anim;
    [SerializeField] protected float moveSpeed;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;

    [Header("Collier Info")]
    [SerializeField] protected LayerMask enemyLayer;
    [SerializeField] protected float circleRadius;
    protected Transform nearEnemy;
    protected Vector3 direc;

    [Header("Weapon Info")]
    [SerializeField] protected WeaponType currentWeaponType;
    [SerializeField] protected Transform holdWeapon;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected float timeDestroy;
    protected WeaponData weaponData;
    protected Bullet bulletOjb;

    public Animator Anim { get => anim; set => anim = value; }

    public virtual void Start()
    {
        OnInit();
        SpawnWeapon();
    }

    public virtual void Update()
    {
        if (isDead)
        {
            return;
        }

        FindCloseEnemy();
        if (isIdle && !isAttack && nearEnemy != null)
        {
            Attack();
        }
    }

    private void SpawnWeapon()
    {
        Instantiate(weaponData.weapon, holdWeapon);
    }

    public void Attack()
    {
        isAttack = true;
        Anim.SetBool(ConstString.IS_ATTACK_STRING, true);
        SpawnBullet();
        Invoke(nameof(ResetAttack), 1f);
        bulletOjb.OnDespawn(timeDestroy);
    }

    public void SpawnBullet()
    {
        direc = nearEnemy.position - transform.position;
        Bullet spawnBullet = LeanPool.Spawn(weaponData.bullet, firePos.position, firePos.rotation);

        bulletOjb = spawnBullet.GetComponent<Bullet>();
        bulletOjb.SeekDirec(direc);
        holdWeapon.gameObject.SetActive(false);
    }

    public void ResetAttack()
    {
        isAttack = false;
        holdWeapon.gameObject.SetActive(true);
        Anim.SetBool(ConstString.IS_ATTACK_STRING, false);
    }

    public virtual void OnInit()
    {
        isDead = false;
        isIdle = true;
        Anim.SetBool(ConstString.IS_IDLE_STRING, true);
        int characterLayer = LayerMask.NameToLayer(ConstString.CHARACTER_LAYER);
        gameObject.layer = characterLayer;
    }

    public virtual void OnDeath()
    {
        isDead = true;
        isIdle = false;
        Anim.SetBool(ConstString.IS_DEAD_STRING, true);
        int defaultLayer = LayerMask.NameToLayer(ConstString.DEFAULT_LAYER);
        gameObject.layer = defaultLayer;
        Invoke(nameof(OnDespawn), 2f);
    }

    public virtual void OnDespawn()
    {

    }

    public void FindCloseEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, circleRadius, enemyLayer);
        float miniumDistance = Mathf.Infinity;
        Debug.Log(hitColliders.Length);
        if(hitColliders.Length > 1)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject != this.gameObject)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance <= miniumDistance)
                    {
                        miniumDistance = distance;
                        nearEnemy = collider.transform;
                    }
                }
                else
                {
                    nearEnemy = null;
                }
            }
        }
        else
        {
            nearEnemy = null;
        }

        //Facing enemy if player found them
        if (isIdle)
        {
            transform.LookAt(nearEnemy);
        }

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

}
