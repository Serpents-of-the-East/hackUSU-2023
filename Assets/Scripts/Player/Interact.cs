using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
    private InteractableNPC currentInteraction = null;
    private CollectableItem currentCollectable = null;
    public bool isInteracting = false;
    public PlayerInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteraction != null && currentInteraction.completedTalkingTo)
        {
            isInteracting = false;
            if (currentInteraction.quest != null)
            {
                inventory.AddQuest(currentInteraction.quest);
                currentInteraction.quest = null;
            }
        }

        if (currentCollectable != null && isInteracting)
        {
            Debug.Log("Collected: " + currentCollectable.itemName);
            QuestManagement questManagement = FindObjectOfType<QuestManagement>();
            questManagement.AddKeyItem(currentCollectable.itemName);
            currentCollectable.gameObject.SetActive(false);
            currentCollectable = null;
            isInteracting = false;
        }
    }

    public void OnInteract(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (currentInteraction != null && currentInteraction.interactable)
            {
                isInteracting = true;
                currentInteraction.interactedWith = true;
            }

            if (currentCollectable != null)
            {
                isInteracting = true;
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

        CollectableItem collectable;
        if (other.gameObject.TryGetComponent<CollectableItem>(out collectable))
        {
            currentCollectable = collectable;
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

        CollectableItem collectable;
        if (other.gameObject.TryGetComponent<CollectableItem>(out collectable))
        {
            if (currentCollectable == collectable)
            {
                currentCollectable = null;
            }
        }
    }
}
