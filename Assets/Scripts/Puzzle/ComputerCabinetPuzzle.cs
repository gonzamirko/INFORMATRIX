using System.Collections.Generic;
using UnityEngine;

public class ComputerCabinetPuzzle : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2f;

    [Header("Visual State")]
    [SerializeField] private SpriteRenderer cabinetSpriteRenderer;
    [SerializeField] private Sprite poweredOnSprite;

    [Header("Completion UI")]
    [SerializeField] private GameObject puzzleCompletedPanel;

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

        if (puzzleCompletedPanel != null)
        {
            puzzleCompletedPanel.SetActive(false);
        }
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

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.E))
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

    private void CompletePuzzle()
    {
        isCompleted = true;

        cabinetSpriteRenderer.sprite = poweredOnSprite;

        if (puzzleCompletedPanel != null)
        {
            puzzleCompletedPanel.SetActive(true);
        }

        Time.timeScale = 0f;

        Debug.Log("¡Computadora armada correctamente!");
    }
}