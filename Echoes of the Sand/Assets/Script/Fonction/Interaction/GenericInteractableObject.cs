using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericInteractableObject : InteractableObjectBase
{
    //utile pour des actions qui n'arrivent pas souvent

    [SerializeField] private UnityEvent onInteract;

    public override void Interact()
    {
        onInteract?.Invoke();
    }
}
