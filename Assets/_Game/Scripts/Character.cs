using System.Collections.Generic;
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
    public List<Enemy> enemyList;
    
    public Transform nearEnemy;
    public Vector3 direc;

    [Header("Weapon Info")]
    public Transform holdWeapon;
    public GameObject weapon;
    public GameObject bulletPrefab;
    public Transform firePos;

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
            isAttack = true;
            AttackEnemy();
            anim.SetBool(ConstString.IS_ATTACK_STRING, true);
            Invoke(nameof(ResetAttack), 1f);
        }
    }

    public void AttackEnemy()
    {
        direc = nearEnemy.position - transform.position;
        GameObject spawnBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        Bullet bulletOjb = spawnBullet.GetComponent<Bullet>();
        bulletOjb.SeekDirec(direc);
        holdWeapon.gameObject.SetActive(false);
        
    }

    public void ResetAttack()
    {
        isAttack = false;
        holdWeapon.gameObject.SetActive(true);
        anim.SetBool(ConstString.IS_ATTACK_STRING, false);
    }

    public void IsDead()
    {
        isDead = true;
        isIdle = false;
        anim.SetBool(ConstString.IS_DEAD_STRING,true);
        int defaultLayer = LayerMask.NameToLayer("Default");
        gameObject.layer = defaultLayer;
    }

    public void FindCloseEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, circleRadius, enemyLayer);
        float miniumDistance = Mathf.Infinity;

        if (hitColliders.Length != 0)
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
