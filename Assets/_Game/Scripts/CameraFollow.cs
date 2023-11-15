using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    private void Start()
    {
        offset = transform.position - target.position;

    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, speed * Time.fixedDeltaTime);
        }
    }
}
