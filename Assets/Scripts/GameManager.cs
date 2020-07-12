using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    
    private void Awake()
    {
        if (!player) player = FindObjectOfType<Player>();
        
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
    }
}