using UnityEngine;

public class ComputerCaseInteraction : MonoBehaviour
{
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private FeedbackController feedbackController;

    private void OnMouseDown()
    {
        if (!inventoryController.HasAllItems())
        {
            feedbackController.ShowMessage("Todavía faltan componentes para armar la computadora.");
            return;
        }

        feedbackController.ShowMessage("¡Computadora armada correctamente!");
    }
}