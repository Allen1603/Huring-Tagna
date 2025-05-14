using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    private ItemPickup itemInRange;

    private PlayerInputAction uiInputActions;
    private bool inputTrigger = false;
    private void Awake()
    {
        uiInputActions = new PlayerInputAction();
        uiInputActions.UI.Pickup.performed += ctx => inputTrigger = true;
    }

    private void OnEnable() => uiInputActions.Enable();
    private void OnDisable() => uiInputActions.Disable();
    void Update()
    {
        if (itemInRange != null && inputTrigger)
        {
            PickupItem(itemInRange);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ItemPickup itemPickup = other.GetComponent<ItemPickup>();
        if (itemPickup != null)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= pickupRange)
            {
                itemInRange = itemPickup;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (itemInRange != null && other.gameObject == itemInRange.gameObject)
        {
            itemInRange = null;
        }
    }

    void PickupItem(ItemPickup item)
    {
        if (item.itemData != null)
        {
            InventoryManager.Instance.AddItem(item.itemData);
            Debug.Log("Picked up " + item.itemData.itemName);
        }
        else
        {
            Debug.LogWarning("This item has no InventoryItem assigned!");
        }

        Destroy(item.gameObject);
        itemInRange = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
