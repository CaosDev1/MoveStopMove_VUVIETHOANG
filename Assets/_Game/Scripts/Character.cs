using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Move Info")]
    public Rigidbody rb;
    public DynamicJoystick joystick;
    public Animator anim;
    public float moveSpeed;
    public bool isIdle;
    public bool isAttack = false;
    public bool isDead = false;

    [Header("Collier Info")]
    public LayerMask enemyLayer;
    public float circleRadius;
    public Transform nearEnemy;
    public Vector3 direc;

    [Header("Weapon Info")]
    public Transform holdWeapon;
    public GameObject weapon;
    public GameObject bulletPrefab;
    public Transform firePos;
    private Bullet bulletOjb;
    private float distancePlayerVsBullet;
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

        Attack();

        DistancePlayerAndBullet();
    }

    public void Attack()
    {
        if (isIdle && !isAttack && nearEnemy != null)
        {
            isAttack = true;
            anim.SetBool(ConstString.IS_ATTACK_STRING, true);
            SpawnBullet();
            Invoke(nameof(ResetAttack), 2f);
        }
    }

    public void SpawnBullet()
    {
        direc = nearEnemy.position - transform.position;
        GameObject spawnBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bulletOjb = spawnBullet.GetComponent<Bullet>();
        bulletOjb.SeekDirec(direc);
        bulletOjb.SeekDistance(distancePlayerVsBullet);
        holdWeapon.gameObject.SetActive(false);
    }

    public void ResetAttack()
    {
        isAttack = false;
        holdWeapon.gameObject.SetActive(true);
        anim.SetBool(ConstString.IS_ATTACK_STRING, false);
    }

    public void DistancePlayerAndBullet()
    {
        if (bulletOjb != null)
        {
            distancePlayerVsBullet = Vector3.Distance(transform.position, bulletOjb.transform.position);
            Debug.Log(distancePlayerVsBullet);
        }
    }

    public void IsDead()
    {
        isDead = true;
        isIdle = false;

        anim.SetBool(ConstString.IS_DEAD_STRING, true);
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
