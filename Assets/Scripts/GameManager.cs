using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Obstacle obstacles;
    [SerializeField] private Player player;
    
    public float NextObstaclePoint { get; private set; }

    private void Awake()
    {
        player.ObstaclePassed += OnObstaclePassed;
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        player.ObstaclePassed -= OnObstaclePassed;
    }

    private void OnObstaclePassed()
    {
        Debug.LogWarning($"[Taniolo] Obstacle Passed");
    }
}