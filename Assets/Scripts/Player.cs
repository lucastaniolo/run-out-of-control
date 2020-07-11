using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask obstacleLayer;
    
    [SerializeField] private Transform[] frontRays;
    
    private Transform tr;
    
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int GrowHash = Animator.StringToHash("Grow");
    private static readonly int ShrinkHash = Animator.StringToHash("Shrink");

    private float JumpForce => jumpForce;

    public event Action ObstaclePassed;
    
    private void Awake()
    {
        tr = transform;
        InputButton.InputUsed += OnInput;
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        InputButton.InputUsed -= OnInput;
    }

    private void Update()
    {

    }
    
    private void FixedUpdate()
    { 
        body.MovePosition(tr.position + tr.right * (speed * Time.deltaTime));

        foreach (var r in frontRays)
        {
            if (Physics.Raycast(r.position, r.TransformDirection(Vector3.right), out var hit, Mathf.Infinity, obstacleLayer))
            {
                Debug.LogWarning($"[Taniolo] distance {hit.distance}");

                if (hit.distance < 0.1f)
                {
                    Debug.LogWarning($"[Taniolo] DIE");
                    Destroy(gameObject);
                }
            }
        }

        
        // if(body.velocity.y < 0)
        //     body.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        
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
                break;
            
            case InputType.Shrink:
                animator.SetTrigger(ShrinkHash);
                break;
            
            case InputType.Shoot:
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
    

}