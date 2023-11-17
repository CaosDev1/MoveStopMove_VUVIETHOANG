using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float rangeDestroy;
    private float distance;
    private Vector3 direction;

    private void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SeekDistance(float newDistance)
    {
        this.distance = newDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(ConstString.ENEMY_TAG) || other.CompareTag(ConstString.PLAYER_TAG))
        {
            Character character = other.GetComponent<Character>();
            character.IsDead();
            Destroy(gameObject);
        }else if(distance > rangeDestroy)
        {
            Destroy(gameObject);
        }
    }
}
