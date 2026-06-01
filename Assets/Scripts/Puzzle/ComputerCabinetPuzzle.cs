using System.Collections.Generic;
using UnityEngine;

public class ComputerCabinetPuzzle : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;

    private bool isCompleted;

    private GameObject player;

    private readonly List<string> requiredItems = new()
    {
        "motherboard",
        "cpu",
        "ram",
        "ssd",
        "psu",
        "gpu"
    };

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isCompleted)
        {
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.transform.position
        );

        if (distance <= interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bool hasAllItems = true;

                foreach (string itemId in requiredItems)
                {
                    if (!InventoryManager.Instance.HasItem(itemId))
                    {
                        Debug.Log("Falta: " + itemId);
                        hasAllItems = false;
                        
                    }
                }

                if (!hasAllItems)
                {
                    FeedbackUI.Instance.ShowMessage("Todavía faltan componentes.");
                    Debug.Log("Todavía faltan componentes.");
                    return;
                }

                CompletePuzzle();
            }
        }
    }

    private void CompletePuzzle()
    {
        isCompleted = true;

        Debug.Log("¡Computadora armada correctamente!");
        FeedbackUI.Instance.ShowMessage("¡Computadora armada correctamente!");
    }
}
