using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    private ItemPickup itemInRange;

    private PlayerInputAction inputActions;
    private bool pickupTriggered = false;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        inputActions.UI.Pickup.performed += ctx => pickupTriggered = true;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Update()
    {
        if (pickupTriggered && itemInRange != null)
        {
            PickupItem(itemInRange);
            pickupTriggered = false;
        }
    }

    void PickupItem(ItemPickup item)
    {
        if (item == null)
        {
            Debug.LogWarning("No item to pick up.");
            return;
        }

        if (item.itemData != null)
        {
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddItem(item.itemData);
                Debug.Log("Picked up: " + item.itemData.itemName);
            }
            else
            {
                Debug.LogError("InventoryManager.Instance is null!");
            }
        }
        else
        {
            Debug.LogWarning("ItemPickup is missing itemData.");
        }

        Destroy(item.gameObject);
        itemInRange = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemPickup item))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= pickupRange)
            {
                itemInRange = item;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (itemInRange != null && other.gameObject == itemInRange.gameObject)
        {
            itemInRange = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
