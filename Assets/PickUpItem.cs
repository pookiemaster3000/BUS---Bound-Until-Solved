using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public string itemName = "Item";

    public void Pickup()
    {
        InventoryManager inventory = FindFirstObjectByType<InventoryManager>();

        if (inventory != null)
        {
            inventory.AddItem(gameObject);
        }
    }
}