using UnityEngine;
using Lean.Pool;
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
    [SerializeField] protected Transform holdWeapon;
    [SerializeField] protected GameObject weapon;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Transform firePos;
    protected Bullet bulletOjb;
    protected float distancePlayerVsBullet;

    public Animator Anim { get => anim; set => anim = value; }

    public virtual void Start()
    {
        Instantiate(weapon, holdWeapon);
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

    public void Attack()
    {
        isAttack = true;
        Anim.SetBool(ConstString.IS_ATTACK_STRING, true);
        SpawnBullet();
        Invoke(nameof(ResetAttack), 2f);
    }

    public void SpawnBullet()
    {
        direc = nearEnemy.position - transform.position;
        GameObject spawnBullet = LeanPool.Spawn(bulletPrefab, firePos.position, firePos.rotation);
        Debug.Log(spawnBullet.name);
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

    public void IsDead()
    {
        isDead = true;
        isIdle = false;
        Anim.SetBool(ConstString.IS_DEAD_STRING, true);
        int defaultLayer = LayerMask.NameToLayer(ConstString.DEFAULT_LAYER);
        gameObject.layer = defaultLayer;
        Invoke(nameof(OnDespawn), 2f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    public void FindCloseEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, circleRadius, enemyLayer);
        float miniumDistance = Mathf.Infinity;

        if (hitColliders.Length > 1)
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject != this.gameObject)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < miniumDistance)
                    {
                        miniumDistance = distance;
                        nearEnemy = collider.transform;
                    }
                }
            }
            //Facing enemy if player found them
            if (isIdle)
            {
                transform.LookAt(nearEnemy);
            }
        }
        else
        {
            nearEnemy = null;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

}
