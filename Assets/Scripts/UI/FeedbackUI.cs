using TMPro;
using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    public static FeedbackUI Instance;

    [SerializeField] private TextMeshProUGUI feedbackText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        feedbackText.text = message;
    }
}
