using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<InventorySlot> inventorySlots;
    public int maxSlots = 20;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance
        }
        else
        {
            Instance = this;
        }
    }

    public void AddItem(InventoryItem item)
    {
        // Check if item already exists
        foreach (var slot in inventorySlots)
        {
            if (slot.item == item)
            {
                slot.quantity++;
                return;
            }
        }

        // Add to new slot
        if (inventorySlots.Count < maxSlots)
        {
            inventorySlots.Add(new InventorySlot { item = item, quantity = 1 });
        }
    }
}
