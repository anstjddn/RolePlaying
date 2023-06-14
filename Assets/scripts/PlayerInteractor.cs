using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] Transform transform;
    [SerializeField] float Range;
    [SerializeField] bool debug;

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range);
        foreach(Collider collider in colliders)
        {

            Iinteractorable interactorable = collider.GetComponent<Iinteractorable>();
            interactorable?.Interact();
        }
    }

    public void OnInteract(InputValue value)
    {
        Interact();
    }
    private void OnDrawGizmosSelected()
    {
        if (!debug)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
