using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;

    private void OnEnable()
    {
        if (AudioManager.Instance == null) return;
        if (volumeSlider != null)
            volumeSlider.SetValueWithoutNotify(AudioManager.Instance.GetVolume());
        if (muteToggle != null)
            muteToggle.SetIsOnWithoutNotify(AudioManager.Instance.GetMuted());
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
