using UnityEngine;
using TMPro;

public class FeedbackController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float messageDuration = 2.5f;

    private float timer;

    private void Start()
    {
        ShowMessage("Buscá los componentes de la computadora.");
    }

    private void Update()
    {
        if (timer <= 0f) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ClearMessage();
        }
    }

    public void ShowMessage(string message)
    {
        feedbackText.text = message;
        timer = messageDuration;
    }

    private void ClearMessage()
    {
        feedbackText.text = "";
    }
}