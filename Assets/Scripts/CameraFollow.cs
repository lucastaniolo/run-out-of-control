using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    
    // public float smoothTime = 0.3f;
    private Vector3 velocity;

    private Transform tr;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Player>().transform;

        tr = transform;
    }

    private void Update()
    {
        var targetPosition = new Vector3(target.position.x - offset.x, tr.position.y, tr.position.z);
        tr.position = targetPosition;
    }
}
