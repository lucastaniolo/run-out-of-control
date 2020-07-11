using System;
using UnityEngine;

public class InputPickUp : MonoBehaviour
{
    [SerializeField] private InputType inputType;

    public InputType InputType => inputType;

    public static event Action<InputPickUp> InputPickUpEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputPickUpEvent?.Invoke(this);
            Debug.LogWarning($"[Taniolo] Picked up {InputType}");
            Destroy(gameObject);
        }
    }
}
