using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;
    private bool isIdle;
    private bool isAttack;

    [Header("Collier Info")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float circleRadius;
    [SerializeField] private List<Enemy> enemyList;
    private Transform target;
    private Transform nearEnemy;

    [Header("Weapon Info")]
    [SerializeField] private Transform holdWeapon;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePos;

    private void Start()
    {
        Instantiate(weapon, holdWeapon);
    }

    private void Update()
    {
        FindCloseEnemy();
        AttackEnemy();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            isIdle = false;
            anim.SetBool("IsIdle", isIdle);
        }
        else
        {
            isIdle = true;
            anim.SetBool("IsIdle", isIdle);

        }
    }

    private void AttackEnemy()
    {
        if (isIdle && target != null)
        {
            transform.LookAt(target.position);
            GameObject bulletOjb = Instantiate(bulletPrefab,firePos.transform.position,firePos.transform.rotation);
            Bullet bullets = bulletOjb.GetComponent<Bullet>();
            bullets.SeekTarget(target);


        }
    }

    private void FindCloseEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, circleRadius, enemyLayer);
        float miniumDistance = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < miniumDistance)
            {
                miniumDistance = distance;
                nearEnemy = collider.transform;
                
            }
        }

        if (nearEnemy != null)
        {
            target = nearEnemy;
            Debug.DrawLine(transform.position, target.position, Color.red);
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

}
