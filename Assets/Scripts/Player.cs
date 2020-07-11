using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // [SerializeField] private ObstacleHandler obstacleHandler;
    
    [SerializeField] private Rigidbody body;
    [SerializeField] private float speed;

    public event Action ObstaclePassed;
    
    private void Awake()
    {
        // obstacleHandler.ObstaclePassed += OnObstaclePassed;
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        // obstacleHandler.ObstaclePassed -= OnObstaclePassed;
    }

    private void FixedUpdate()
    {
        // body.MovePosition();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
    
    private void OnObstaclePassed()
    {
        
    }
}