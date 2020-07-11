using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float airJumpForce = 4f;

    // [SerializeField] private float fallMultiplier = 3f;

    private Transform tr;

    private float JumpForce => body.velocity.y > 0 ? airJumpForce : jumpForce;

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
        body.MovePosition(tr.position + tr.forward * (speed * Time.deltaTime));

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
                body.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                break;
            
            case InputType.Grow:
                break;
            
            case InputType.Shrink:
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