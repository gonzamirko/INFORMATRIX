using UnityEngine;


public class ClickableItem : MonoBehaviour
{
    [SerializeField] private string itemId;
    [SerializeField] private string displayName;

    [SerializeField] private float interactionDistance = 1.5f;

    private bool wasCollected;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (wasCollected)
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
                wasCollected = true;

                InventoryManager.Instance.CollectItem(itemId);

                FeedbackUI.Instance.ShowMessage("Encontraste: " + displayName);

                Debug.Log("Recogiste: " + displayName);

                gameObject.SetActive(false);
            }
        }
    }
}
