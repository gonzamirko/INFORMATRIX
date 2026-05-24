using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    [SerializeField] private float delaySeconds = 2.5f;

    private void Start()
    {
        Invoke(nameof(GoToMainMenu), delaySeconds);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}