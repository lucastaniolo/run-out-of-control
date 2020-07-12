using System;
using System.Collections;
using UnityEngine;

public class InputPickUp : MonoBehaviour
{
    [SerializeField] private InputType inputType;
    [SerializeField] private Animator animator;

    public InputType InputType => inputType;

    public static event Action<InputPickUp> InputPickUpEvent;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 1f));
        animator.SetTrigger("Play");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InputPickUpEvent?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
