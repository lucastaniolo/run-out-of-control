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

    [SerializeField] private GameObject finalHud;
    [SerializeField] private GameObject restartHud;

    public bool IsBig { get; set; } = true;
    public bool IsGrounded { get; set; }

    private float lastGroundedTime;

    public bool CanJump => IsGrounded || Time.time - lastGroundedTime < 0.15f;

    private Transform tr;
    
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int IsBigHash = Animator.StringToHash("IsBig");
    private static readonly int DieHash = Animator.StringToHash("Die");
    private static readonly int WinHash = Animator.StringToHash("Win");

    private float JumpForce => IsBig ? growJumpForce : shrinkJumpForce;

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

        if (Physics.Raycast(tr.position, tr.TransformDirection(Vector3.down), out var hitGround, Mathf.Infinity))
        {
            IsGrounded = hitGround.distance < 0.65f;

            if (IsGrounded)
                lastGroundedTime = Time.time;
        }
        
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

    private bool jumpCooldown;

    private void Jump()
    {
        if (!CanJump && jumpCooldown) return;

        jumpCooldown = true;
        // animator.SetTrigger(JumpHash);
        var currentVelocity = body.velocity;
        body.velocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        Invoke(nameof(ReleaseJumpCooldown), 0.5f);
    }

    private void ReleaseJumpCooldown() => jumpCooldown = false;

    private void Shoot()
    {
        if (Physics.Raycast(tr.position, tr.TransformDirection(Vector3.right), out var hit, Mathf.Infinity, obstacleLayer))
        {
            Debug.LogWarning($"[Taniolo] SHOOT {hit.transform.name}");

            if (hit.transform.TryGetComponent<Box>(out var box))
                box.Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            animator.SetTrigger(WinHash);
            isDead = true;
            finalHud.SetActive(true);
        }
        else if (other.CompareTag("Kill"))
        {
            animator.SetTrigger(DieHash);
            isDead = true;
            restartHud.SetActive(true);
        }
    }
}