using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventorySlot> inventorySlots = new List<InventorySlot>();
    public int maxSlots = 20;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(InventoryItem item)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item == item)
            {
                slot.quantity++;
                return;
            }
        }

        if (inventorySlots.Count < maxSlots)
        {
            inventorySlots.Add(new InventorySlot { item = item, quantity = 1 });
        }
        else
        {
            Debug.Log("Inventory full!");
        }
    }
}
