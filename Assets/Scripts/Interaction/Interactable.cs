using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;

    // Message appears in front of interactable
    public string message;

    public void BaseInteraact()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }

        Interact();
    }

    protected virtual void Interact()
    {
        // Codeless - to be overridden by subclasses
    }
}
