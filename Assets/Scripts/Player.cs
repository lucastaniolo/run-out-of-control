using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    [SerializeField] private float growJumpForce = 15f;
    [SerializeField] private float shrinkJumpForce = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider boxCollider;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [SerializeField] private Transform[] frontRays;

    public bool IsBig { get; set; } = true;
    
    private Transform tr;
    
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int GrowHash = Animator.StringToHash("Grow");
    private static readonly int ShrinkHash = Animator.StringToHash("Shrink");

    private float JumpForce => IsBig ? growJumpForce : shrinkJumpForce;

    public event Action ObstaclePassed;
    
    private void Awake()
    {
        tr = transform;
        InputButton.InputUsed += OnInput;
    }
    
    private void OnDestroy()
    {
        InputButton.InputUsed -= OnInput;
    }

    private void FixedUpdate()
    { 
        body.MovePosition(tr.position + tr.right * (speed * Time.deltaTime));

        foreach (var r in frontRays)
        {
            if (Physics.Raycast(r.position, r.TransformDirection(Vector3.right), out var hit, Mathf.Infinity, obstacleLayer))
            {
                // Debug.LogWarning($"[Taniolo] distance {hit.distance}");

                if (hit.distance < 0.1f)
                    Destroy(gameObject);
            }
        }
    }

    private void OnInput(InputButton inputButton)
    {
        switch (inputButton.InputType)
        {
            case InputType.Jump:
                Jump();
                break;
            
            case InputType.Grow:
                animator.SetTrigger(GrowHash);
                IsBig = true;
                boxCollider.size = Vector3.one;
                break;
            
            case InputType.Shrink:
                animator.SetTrigger(ShrinkHash);
                IsBig = false;
                boxCollider.size = Vector3.one * 0.5f;
                break;
            
            case InputType.Shoot:
                Shoot();
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Jump()
    {
        if (Physics.Raycast(tr.position, tr.TransformDirection(Vector3.down), out var hit, Mathf.Infinity))
        {
            Debug.LogWarning($"[Taniolo] distance {hit.distance}");
                    
            if (hit.distance > 0.65f)
                return;
        }
                
        animator.SetTrigger(JumpHash);
        var currentVelocity = body.velocity;
        body.velocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    private void Shoot()
    {
        if (Physics.Raycast(tr.position, tr.TransformDirection(Vector3.right), out var hit, Mathf.Infinity, obstacleLayer))
        {
            Debug.LogWarning($"[Taniolo] SHOOT {hit.transform.name}");

            if (hit.transform.TryGetComponent<Box>(out var box))
                box.Explode();
        }
    }
}