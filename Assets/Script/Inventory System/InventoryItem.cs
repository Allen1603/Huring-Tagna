using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Texture2D iconTexture;
    //public GameObject prefab; // Optional for 3D models
}
[System.Serializable]
public class InventorySlot
{
    public InventoryItem item;
    public int quantity;
}
