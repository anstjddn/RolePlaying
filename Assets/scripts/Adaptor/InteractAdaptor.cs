using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractAdaptor : MonoBehaviour, Iinteractorable
{
    public UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
