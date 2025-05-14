using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public float pickupRange = 3f;
    public KeyCode pickupKey = KeyCode.E;
    private ItemPickup itemInRange;

    void Update()
    {
        if (itemInRange != null && Input.GetKeyDown(pickupKey))
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
                Debug.Log("Press E to pick up " + itemPickup.itemData.itemName);
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
