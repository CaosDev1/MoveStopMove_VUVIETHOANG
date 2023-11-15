using UnityEngine;

public class Bullet : MonoBehaviour
{
    //public Transform target;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private Vector3 direction;

    private void Update()
    {
        rb.velocity = direction.normalized * speed;
    }

    public void SeekDirec(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }


}
