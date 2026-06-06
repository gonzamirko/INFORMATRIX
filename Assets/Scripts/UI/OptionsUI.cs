using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;

    private void OnEnable()
    {
        if (AudioManager.Instance == null)
        {
            return;
        }

        if (volumeSlider != null)
        {
            volumeSlider.SetValueWithoutNotify(AudioManager.Instance.GetVolume());
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (muteToggle != null)
        {
            muteToggle.SetIsOnWithoutNotify(AudioManager.Instance.GetMuted());
            muteToggle.onValueChanged.RemoveListener(OnMuteToggled);
            muteToggle.onValueChanged.AddListener(OnMuteToggled);
        }
    }

    private void OnDisable()
    {
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }

        if (muteToggle != null)
        {
            muteToggle.onValueChanged.RemoveListener(OnMuteToggled);
        }
    }

    public void OnVolumeChanged(float value)
    {
        AudioManager.Instance?.SetVolume(value);
    }

    public void OnMuteToggled(bool muted)
    {
        AudioManager.Instance?.SetMute(muted);
    }
}
