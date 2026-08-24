using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxInventorySize = 4;

    private List<GameObject> inventory = new List<GameObject>();

    public bool AddItem(GameObject item)
    {
        if (inventory.Count >= maxInventorySize)
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        inventory.Add(item);

        // Hide the item while it is in the inventory
        item.SetActive(false);

        Debug.Log("Picked up: " + item.name);
        Debug.Log("Inventory: " + inventory.Count + "/" + maxInventorySize);

        return true;
    }

    public bool IsFull()
    {
        return inventory.Count >= maxInventorySize;
    }

    public int GetItemCount()
    {
        return inventory.Count;
    }

    public List<GameObject> GetInventory()
    {
        return inventory;
    }

    public void DropLastItem(Transform playerCamera)
    {
        if (inventory.Count == 0)
        {
            Debug.Log("Inventory is empty!");
            return;
        }

        // Get the last item in the inventory
        GameObject item = inventory[inventory.Count - 1];

        // Remove it from the inventory
        inventory.RemoveAt(inventory.Count - 1);

        // Make the item visible again
        item.SetActive(true);

        // Place it in front of the player
        item.transform.position =
            playerCamera.position + playerCamera.forward * 2f;

        Debug.Log("Dropped: " + item.name);
        Debug.Log("Inventory: " + inventory.Count + "/" + maxInventorySize);
    }
}