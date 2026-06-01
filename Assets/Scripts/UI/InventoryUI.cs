using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private TextMeshProUGUI inventoryText;

    private string currentText = "Inventario:\n";

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(string itemName)
    {
        currentText += "- " + itemName + "\n";

        inventoryText.text = currentText;
    }
}
