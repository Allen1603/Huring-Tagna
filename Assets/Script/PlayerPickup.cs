using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public KeyCode pickupKey = KeyCode.E;
    private GameObject itemInRange;

    void Update()
    {
        if (itemInRange != null && Input.GetKeyDown(pickupKey))
        {
            PickupItem(itemInRange);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= pickupRange)
            {
                itemInRange = other.gameObject;
                Debug.Log("Press E to pick up " + itemInRange.name);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == itemInRange)
        {
            itemInRange = null;
        }
    }

    void PickupItem(GameObject item)
    {
        ItemPickup pickupComponent = item.GetComponent<ItemPickup>();
        if (pickupComponent != null)
        {
            InventoryItem inventoryItem = pickupComponent.item;
            InventoryManager.Instance.AddItem(inventoryItem);
            Debug.Log("Picked up " + inventoryItem.itemName);
        }
        else
        {
            Debug.LogWarning("Item does not have ItemPickup script attached.");
        }

        Destroy(item);
        itemInRange = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
