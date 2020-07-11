using System;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private Collider trigger;

    public event Action ObstaclePassed;
    
    private void OnTriggerEnter(Collider other)
    {
        ObstaclePassed?.Invoke();
    }
}