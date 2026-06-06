using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip puzzleMusic;

    [SerializeField] private float menuMusicVolumeMultiplier = 1f;
    [SerializeField] private float puzzleMusicVolumeMultiplier = 0.55f;

    private AudioSource audioSource;
    private float userVolume;
    private float currentTrackMultiplier = 1f;

    private const string VolumeKey = "MusicVolume";
    private const string MuteKey = "MusicMuted";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.mute = PlayerPrefs.GetInt(MuteKey, 0) == 1;

        userVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        if (Instance != this) return;
        PlayClipForScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayClipForScene(scene.name);
    }

    private void PlayClipForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "MainMenuScene":
                PlayClip(menuMusic, menuMusicVolumeMultiplier);
                break;
            case "PuzzleUno":
                PlayClip(puzzleMusic, puzzleMusicVolumeMultiplier);
                break;
        }
    }

    private void PlayClip(AudioClip clip, float multiplier)
    {
        currentTrackMultiplier = multiplier;
        audioSource.volume = userVolume * currentTrackMultiplier;
        if (clip == null || audioSource.clip == clip) return;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public float GetVolume() => userVolume;
    public bool GetMuted() => audioSource.mute;

    public void SetVolume(float value)
    {
        userVolume = value;
        audioSource.volume = userVolume * currentTrackMultiplier;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    public void SetMute(bool muted)
    {
        audioSource.mute = muted;
        PlayerPrefs.SetInt(MuteKey, muted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
