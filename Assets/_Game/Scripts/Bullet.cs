using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rangeDestroy;
    [SerializeField] private float timeDestroy;
    private Vector3 enemyPos;
    private Vector3 direction;
    
    private void Update()
    {
        rb.velocity = direction.normalized * speed;

        Invoke(nameof(OnDespawn), timeDestroy);
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstString.ENEMY_TAG) || other.CompareTag(ConstString.PLAYER_TAG))
        {
            Character character = other.GetComponent<Character>();
            character.IsDead();
            OnDespawn();
        }
    }
}
