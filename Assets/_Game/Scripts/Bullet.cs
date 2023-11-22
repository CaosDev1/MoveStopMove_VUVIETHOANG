using UnityEditor;
using UnityEngine;
using Lean.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rangeDestroy;
    [SerializeField] private float timeDestroy;
    private Vector3 direction;
    
    private void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }

    public void OnDespawn(float timeDespawn)
    {
        LeanPool.Despawn(gameObject, timeDespawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstString.ENEMY_TAG) || other.CompareTag(ConstString.PLAYER_TAG))
        {
            Character character = other.GetComponent<Character>();
            character.IsDead();
            LeanPool.Despawn(gameObject);
        }
    }
}
