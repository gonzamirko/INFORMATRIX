using UnityEngine;

public class CollectibleHardwareItem : MonoBehaviour
{
    [SerializeField] private HardwareItemType itemType;
    [SerializeField] private InventoryController inventoryController;

    private void OnMouseDown()
    {
        inventoryController.AddItem(itemType);
        gameObject.SetActive(false);
    }
}