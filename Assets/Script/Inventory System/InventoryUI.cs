using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;

    private List<GameObject> slotObjects = new List<GameObject>();

    private void Start()
    {
        InitializeSlots();
    }

    private void Update()
    {
        UpdateUI();
    }

    void InitializeSlots()
    {
        // Create exactly 20 slots based on maxSlots
        for (int i = 0; i < InventoryManager.Instance.maxSlots; i++)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            slotObjects.Add(obj);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slotObjects.Count; i++)
        {
            GameObject obj = slotObjects[i];
            Image icon = obj.transform.Find("Icon").GetComponent<Image>();
            Text quantityText = obj.transform.Find("Quantity").GetComponent<Text>();

            if (i < InventoryManager.Instance.inventorySlots.Count)
            {
                var slot = InventoryManager.Instance.inventorySlots[i];
                icon.sprite = slot.item.icon;
                icon.enabled = true;
                quantityText.text = slot.quantity.ToString();
            }
            else
            {
                icon.sprite = null;
                icon.enabled = false;
                quantityText.text = "";
            }
        }
    }
}
