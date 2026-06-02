using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private readonly HashSet<string> collectedItemIds = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void CollectItem(string itemId)
    {
        if (collectedItemIds.Contains(itemId))
        {
            return;
        }

        collectedItemIds.Add(itemId);

        //Debug.Log("Encontraste: " + itemId);
    }

    public bool HasItem(string itemId)
    {
        return collectedItemIds.Contains(itemId);
    }
}
