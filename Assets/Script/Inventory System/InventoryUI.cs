using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotParent;

    private List<GameObject> slotObjects = new();
    private bool isOpen = false;

    private PlayerInputAction uiInputActions;

    private void Awake()
    {
        uiInputActions = new PlayerInputAction();
        uiInputActions.UI.Inventory.performed += ctx => ToggleInventory();
    }

    private void OnEnable() => uiInputActions.Enable();
    private void OnDisable() => uiInputActions.Disable();

    private void Start()
    {
        InitializeSlots();
        inventoryPanel.SetActive(false); // Hide on start
    }

    void ToggleInventory()
    {
        inventoryPanel.SetActive(isOpen);
        UpdateUI();
    }

    void InitializeSlots()
    {
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

            Transform iconTransform = obj.transform.Find("Icon");
            Transform quantityTransform = obj.transform.Find("Quantity");

            if (iconTransform == null || quantityTransform == null)
            {
                Debug.LogWarning($"Slot {i} is missing Icon or Quantity child objects.");
                continue;
            }

            RawImage icon = iconTransform.GetComponent<RawImage>();
            TextMeshProUGUI quantityText = quantityTransform.GetComponent<TextMeshProUGUI>();

            if (i < InventoryManager.Instance.inventorySlots.Count)
            {
                var slot = InventoryManager.Instance.inventorySlots[i];
                if (slot.item != null)
                {
                    icon.texture = slot.item.iconTexture;
                    icon.enabled = true;
                    quantityText.text = slot.quantity.ToString();
                }
                else
                {
                    icon.texture = null;
                    icon.enabled = false;
                    quantityText.text = "";
                }
            }
            else
            {
                icon.texture = null;
                icon.enabled = false;
                quantityText.text = "";
            }
        }
    }
}
