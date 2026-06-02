using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI motherboardText;
    [SerializeField] private TextMeshProUGUI cpuText;
    [SerializeField] private TextMeshProUGUI ramText;
    [SerializeField] private TextMeshProUGUI storageText;
    [SerializeField] private TextMeshProUGUI powerSupplyText;
    [SerializeField] private TextMeshProUGUI graphicsCardText;

    [Header("Feedback")]
    [SerializeField] private FeedbackController feedbackController;

    private readonly HashSet<HardwareItemType> collectedItems = new();

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void AddItem(HardwareItemType itemType)
    {
        if (collectedItems.Contains(itemType))
        {
            feedbackController.ShowMessage("Ya tenés ese componente.");
            return;
        }

        collectedItems.Add(itemType);
        UpdateInventoryUI();

        feedbackController.ShowMessage($"Agarraste: {GetItemDisplayName(itemType)}");
    }

    public bool HasAllItems()
    {
        return collectedItems.Count >= 6;
    }

    private void UpdateInventoryUI()
    {
        motherboardText.text = $"Placa madre: {GetStatus(HardwareItemType.Motherboard)}";
        cpuText.text = $"Microprocesador: {GetStatus(HardwareItemType.Cpu)}";
        ramText.text = $"Memoria RAM: {GetStatus(HardwareItemType.Ram)}";
        storageText.text = $"Disco/SSD: {GetStatus(HardwareItemType.Storage)}";
        powerSupplyText.text = $"Fuente: {GetStatus(HardwareItemType.PowerSupply)}";
        graphicsCardText.text = $"Placa gráfica: {GetStatus(HardwareItemType.GraphicsCard)}";
    }

    private string GetStatus(HardwareItemType itemType)
    {
        return collectedItems.Contains(itemType) ? "✓" : "-";
    }

    private string GetItemDisplayName(HardwareItemType itemType)
    {
        return itemType switch
        {
            HardwareItemType.Motherboard => "Placa madre",
            HardwareItemType.Cpu => "Microprocesador",
            HardwareItemType.Ram => "Memoria RAM",
            HardwareItemType.Storage => "Disco/SSD",
            HardwareItemType.PowerSupply => "Fuente de alimentación",
            HardwareItemType.GraphicsCard => "Placa gráfica",
            _ => "Componente"
        };
    }
}