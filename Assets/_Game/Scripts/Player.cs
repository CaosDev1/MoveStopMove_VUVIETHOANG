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
    [SerializeField] private LayerMask botLayerMark;
    [SerializeField] private float circleRadius;

    [Header("Weapon Info")]
    [SerializeField] private Transform holdWeapon;
    [SerializeField] private GameObject weapon;

    private void Start()
    {
        Instantiate(weapon, holdWeapon);
    }

    private void Update()
    {
        AttackBot();
        AnimController();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            isIdle = false;
        }
        else
        {
            isIdle = true;
        }
    }

    private void AttackBot()
    {
        Collider[] bot = Physics.OverlapSphere(transform.position, circleRadius, botLayerMark);
        if(bot.Length != 0 ) 
        {
            if (isIdle)
            {
                transform.LookAt(bot[bot.Length - 1].transform.position);
                isAttack= true;
            }
            else
            {
                isAttack= false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
        
    }
    private void AnimController()
    {
        anim.SetBool("IsIdle", isIdle);
        anim.SetBool("IsAttack", isAttack);
    }
    

}
