using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionDistance = 5f;
    public KeyCode interactKey = KeyCode.E;

    [Header("References")]
    public Transform playerCamera;
    public InventoryManager inventoryManager;

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            TryInteract();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    private void TryInteract()
    {
        // Create a ray from the player's camera
        Ray ray = new Ray(
            playerCamera.position,
            playerCamera.forward
        );

        // Check if the ray hits something within interaction distance
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            // ------------------------------------
            // CHECK FOR PICKUP ITEM
            // ------------------------------------

            PickupItem pickupItem =
                hit.collider.GetComponentInParent<PickupItem>();

            if (pickupItem != null)
            {
                // Make sure we have an inventory manager
                if (inventoryManager == null)
                {
                    Debug.LogWarning("Inventory Manager is not assigned!");
                    return;
                }

                // Try to add the item to the inventory
                if (!inventoryManager.IsFull())
                {
                    pickupItem.Pickup();
                }
                else
                {
                    Debug.Log("Inventory is full! You can only carry 4 items.");
                }

                return;
            }

            // ------------------------------------
            // CHECK FOR NORMAL INTERACTABLE
            // ------------------------------------

            Interactable interactable =
                hit.collider.GetComponentInParent<Interactable>();

            // If an interactable object was found, interact with it
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void DropItem()
    {
        if (inventoryManager == null)
        {
            Debug.LogWarning("Inventory Manager is not assigned!");
            return;
        }

        inventoryManager.DropLastItem(playerCamera);
    }
}