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

    public void Start()
    {
        Instantiate(weapon, holdWeapon);
    }

    public void Update()
    {
        FindCloseEnemy();

        if (isIdle && !isAttack && nearEnemy != null)
        {
            isAttack = true;
            AttackEnemy();
            anim.SetBool("IsAttack", true);
            Invoke(nameof(ResetAttack), 2f);
        }
    }

    public void AttackEnemy()
    {
        direc = nearEnemy.position - transform.position;
        GameObject spawnBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        Bullet bulletOjb = spawnBullet.GetComponent<Bullet>();
        bulletOjb.SeekDirec(direc);
    }

    public void ResetAttack()
    {
        isAttack = false;
        anim.SetBool("IsAttack", false);
    }

    public void FindCloseEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, circleRadius, enemyLayer);
        float miniumDistance = Mathf.Infinity;

        if (hitColliders.Length != 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if(collider.gameObject != this.gameObject)
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

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

}
