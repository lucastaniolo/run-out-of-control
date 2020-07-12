using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    
    public void Explode()
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
