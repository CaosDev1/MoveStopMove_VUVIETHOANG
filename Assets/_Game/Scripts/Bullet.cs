using UnityEditor;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private Character attacker;
    private Vector3 direction;
    
    private void Update()
    {
        rb.velocity = direction.normalized * speed;
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
            Character character = other.GetComponent<Character>();
            if (attacker != character)
            {
                character.OnDeath();
                LeanPool.Despawn(gameObject);
            }
            
        }
    }
}
