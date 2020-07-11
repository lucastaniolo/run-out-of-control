using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Animator animator;

    private Transform tr;
    
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Grow = Animator.StringToHash("Grow");
    private static readonly int Shrink = Animator.StringToHash("Shrink");

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
                animator.SetTrigger(Jump);
                var currentVelocity = body.velocity;
                body.velocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);
                body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                break;
            
            case InputType.Grow:
                animator.SetTrigger(Grow);
                break;
            
            case InputType.Shrink:
                animator.SetTrigger(Shrink);
                break;
            
            case InputType.Shoot:
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void OnObstaclePassed()
    {
        
    }
}