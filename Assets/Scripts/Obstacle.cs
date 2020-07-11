using UnityEngine;

public enum ObstacleType { Jump, Gun, Slide }

public class Obstacle : MonoBehaviour
{
    [SerializeField] private ObstacleType obstacleType;

    public ObstacleType Type => obstacleType;
}