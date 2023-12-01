using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    private Transform myTransform;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;

    private void Start()
    {
        myTransform = transform;
        offset = myTransform.position - target.position;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, target.position + offset, speed * Time.fixedDeltaTime);
        }
    }
}
