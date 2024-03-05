using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjectBase : MonoBehaviour, IInteractable
{


    public abstract void Interact();
}
