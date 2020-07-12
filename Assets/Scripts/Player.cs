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
    
    [SerializeField] private Transform[] bigFrontRays;
    [SerializeField] private Transform[] smallFrontRays;

    public bool IsBig { get; set; } = true;
    
    private Transform tr;
    
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int IsBigHash = Animator.StringToHash("IsBig");
    private static readonly int DieHash = Animator.StringToHash("Die");

    private float JumpForce => IsBig ? growJumpForce : shrinkJumpForce;

    public event Action ObstaclePassed;

    public static event Action Died;

    private bool isDead;
    
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
        if (isDead) return;
        
        body.MovePosition(tr.position + tr.right * (speed * Time.deltaTime));

        var rays = IsBig ? bigFrontRays : smallFrontRays;
        
        foreach (var r in rays)
            if (Physics.Raycast(r.position, r.TransformDirection(Vector3.right), out var hit, Mathf.Infinity,
                obstacleLayer) && hit.distance < 0.1f)
            {
                isDead = true;
                Died?.Invoke();
                animator.SetTrigger(DieHash);
            }
    }

    private void OnInput(InputButton inputButton)
    {
        if (isDead) return;
        
        switch (inputButton.InputType)
        {
            case InputType.Jump:
                Jump();
                break;
            
            case InputType.Grow:
                tr.position += new Vector3(0f, 0.25f, 0f);
                IsBig = true;
                animator.SetBool(IsBigHash, IsBig);
                boxCollider.size = Vector3.one;
                break;
            
            case InputType.Shrink:
                IsBig = false;
                animator.SetBool(IsBigHash, IsBig);
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