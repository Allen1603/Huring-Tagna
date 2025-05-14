using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class ItemPickup : MonoBehaviour
{
    public InventoryItem itemData; // Assign the item ScriptableObject in Inspector
    public float pickupRadius = 3f;

    private Transform player;

    private PlayerInputAction uiInputActions;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Ensure the item has a trigger collider
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }
    private void Awake()
    {
        uiInputActions = new PlayerInputAction();
        uiInputActions.UI.Pickup.performed += ctx => Pickup();
    }
    private void OnEnable() => uiInputActions.Enable();
    private void OnDisable() => uiInputActions.Disable();

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

    }

    void Pickup()
    {
        if (itemData != null)
        {
            InventoryManager.Instance.AddItem(itemData);
            Debug.Log("Picked up: " + itemData.itemName);
        }
        else
        {
            Debug.LogWarning("No item assigned to this pickup!");
        }

        Destroy(gameObject); // Remove the item from the scene
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
