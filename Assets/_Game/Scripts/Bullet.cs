using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;

    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        }
    }

    public void SeekTarget(Transform target)
    {
        this.target = target;
    }


}
