using TMPro;
using UnityEngine;
using System.Collections;

public class FeedbackUI : MonoBehaviour
{
    public static FeedbackUI Instance;

    [SerializeField] private TextMeshProUGUI feedbackText;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        feedbackText.text = message;

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(HideMessageAfterSeconds());
    }

    private IEnumerator HideMessageAfterSeconds()
    {
        yield return new WaitForSeconds(2f);

        feedbackText.text = "";
    }
}
