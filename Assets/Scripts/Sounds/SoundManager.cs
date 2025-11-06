using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource _sfxSpurce;
    [SerializeField] private AudioSource _musicSource;
    public AudioSource MusicSource => _musicSource;

    [Header("Music Settings")]
    public AudioClip mainMenuMusic;      // Полный трек
    //public AudioClip gamePlayMusic;      // Игровая музыка
    public float loopStartTime = 10f; // Время в секундах, с которого начинается зацикливание
    public bool IgnoreLooping = false;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        PlayMusic(mainMenuMusic);
    }

    private void LateUpdate() {
        // Когда трек подходит к концу или минус loopStartTime, включаем зацикливание
        if (!IgnoreLooping && _musicSource.clip != null && _musicSource.time >= _musicSource.clip.length) {
            _musicSource.time = loopStartTime;
            _musicSource.Play();
        }

    }

    public void PlayMusic(AudioClip clip) {
        if (clip == null) return;

        _musicSource.clip = clip;
        _musicSource.loop = false;
        _musicSource.time = 0f;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        if (clip != null) _sfxSpurce.PlayOneShot(clip);
    }

    public void SetMusicVolume(float volume) {
        _musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume) {
        _sfxSpurce.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic() {
        _musicSource.Stop();
    }

}