using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private InteractableNPC currentInteraction = null;
    public bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteraction != null && currentInteraction.completedTalkingTo)
        {
            isInteracting = false;
        }
    }

    public void OnInteract(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            Debug.Log("Pressed");
            if (currentInteraction != null && currentInteraction.interactable)
            {
                isInteracting = true;
                currentInteraction.interactedWith = true;
            }
            else
            {
                currentInteraction.interactedWith = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        InteractableNPC interactableNPC;
        if (other.gameObject.TryGetComponent<InteractableNPC>(out interactableNPC))
        {
            currentInteraction = interactableNPC;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        InteractableNPC interactableNPC;
        if (other.gameObject.TryGetComponent<InteractableNPC>(out interactableNPC))
        {
            if (interactableNPC == currentInteraction)
            {
                currentInteraction = null;
            }
        }
    }
}