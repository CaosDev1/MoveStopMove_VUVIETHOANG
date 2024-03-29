using UnityEditor;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform bulletSkin;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool canRotate;
    private Character attacker;
    private Vector3 direction;
    
    public virtual void Update()
    {
        rb.velocity = direction.normalized * speed;
        RotateWeapon();
    }

    public void RotateWeapon()
    {
        if (canRotate)
        {
            bulletSkin.Rotate(Vector3.forward * rotateSpeed, Space.Self);
        }
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SeekAttacker(Character attacker)
    {
        this.attacker = attacker;

    }

    public void OnDespawn(float timeDespawn)
    {
        LeanPool.Despawn(gameObject, timeDespawn);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CacheString.BOT_TAG) || other.CompareTag(CacheString.PLAYER_TAG))
        {
            Character victim = other.GetComponent<Character>();
            
            if (attacker != victim)
            {
                victim.OnDeath();
                attacker.RemoveTargetWhenHit(victim);
                LeanPool.Despawn(gameObject);
                attacker.ResetAttack();
            }
            
        }
    }
}
