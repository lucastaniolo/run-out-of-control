using UnityEngine;

public class Box : MonoBehaviour
{
    public void Explode()
    {
        Debug.LogWarning($"[Taniolo] Box exploded");
        Destroy(gameObject);
    }
}
