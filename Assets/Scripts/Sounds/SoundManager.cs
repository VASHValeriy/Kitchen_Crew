using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }
    public Slider MusicVolumeSlider { get; private set; }
    public Slider SfxVolumeSlider { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClipsRerfsSO _audioClipsSFXRefsSO;
    public AudioSource MusicSource => _musicSource;

    [Header("Music Settings")]
    public AudioClip mainMenuMusic;     // Полный трек
    private float _loopStartTime = 8f; // Время в секундах, с которого начинается зацикливание

    [Header("SFX Volume Settings")]
    private float _trashVolume = 0.4f;

    public float GetSFXVolume() => _sfxSource.volume;

    public event System.Action<float> OnSfxVolumeChanged;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        _sfxSource.volume = PlayerPrefs.GetFloat("SfxVolume", 0.5f);
        _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        SetSfxVolume(_sfxSource.volume);
        SetMusicVolume(_musicSource.volume);

        if (Player.Instance != null) {
            Player.Instance.OnPickedItem += Player_OnPickedItem;
        }

        CuttingCounter.OnEveryKnifeSound += CuttingCounter_OnEveryKnifeSound;
        BaseCounter.OnObjectPlacement += BaseCounter_OnObjectPlacement;
        TrashCounter.OnDropTrash += TrashCounter_OnDropTrash;

        PlayMusic(mainMenuMusic);
    }

    private void OnEnable() {
        DeliveryManager.OnCreated += RegisterDeliveryEvents;
    }

    private void OnDisable() {
        DeliveryManager.OnCreated -= RegisterDeliveryEvents;
    }

    private void RegisterDeliveryEvents() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void Update() {
        // Проверяем, не тянет ли пользователь слайдер
        if (MusicSliderContorollerUI.Instance != null && MusicSliderContorollerUI.Instance.IsDragging)
            return;

        // Loop logic
        if (_musicSource.time >= _musicSource.clip.length - 0.05f) {
            _musicSource.time = _loopStartTime;
        }
    }

    public void PlayMusic(AudioClip clip) {
        if (clip == null) return;

        _musicSource.clip = clip;
        _musicSource.Play();
    }

    private void PlaySFX(AudioClip[] clipArray, Vector3 position, float volume = 0.5f) {
        if (_sfxSource == null) {
            return;
        }

        if (clipArray == null || clipArray.Length == 0) return;

        if (_sfxSource.gameObject == null) {
            return;
        }
        _sfxSource.transform.position = position;
        _sfxSource.PlayOneShot(clipArray[Random.Range(0, clipArray.Length)], volume * _sfxSource.volume);
    }

    public void PlayStepsOfPlayers(Vector3 position, float volume = 0.5f) {
        PlaySFX(_audioClipsSFXRefsSO.footStep, position, volume * _sfxSource.volume);
    }

    public void PlayWarningSound(Vector3 position) {
        PlaySFX(_audioClipsSFXRefsSO.warning, position);
    }

    public void SetMusicVolume(float volume) {
        _musicSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("MusicVolume", _musicSource.volume);
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float volume) {
        _sfxSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SfxVolume", _sfxSource.volume);
        PlayerPrefs.Save();

        OnSfxVolumeChanged?.Invoke(_sfxSource.volume);
    }

    public void GetVolumeMusicSlider(Slider slider) {
        MusicVolumeSlider = slider;
        if (MusicVolumeSlider != null) {
            MusicVolumeSlider.value = _musicSource.volume; // подтягиваем текущее значение громкости
            MusicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }
    }
    public void GetVolumeSfxSlider(Slider slider) {
        SfxVolumeSlider = slider;
        if (SfxVolumeSlider != null) {
            SfxVolumeSlider.value = _sfxSource.volume; // подтягиваем текущее значение громкости
            SfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        }
    }

    public void StopMusic() => _musicSource.Stop();

    private void TrashCounter_OnDropTrash(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySFX(_audioClipsSFXRefsSO.trash, trashCounter.transform.position, _trashVolume);
    }

    private void BaseCounter_OnObjectPlacement(object sender, System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySFX(_audioClipsSFXRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedItem(object sender, System.EventArgs e) {
        Player playerTransform = Player.Instance;
        PlaySFX(_audioClipsSFXRefsSO.objectPickup, playerTransform.transform.position);
    }

    private void CuttingCounter_OnEveryKnifeSound(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySFX(_audioClipsSFXRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySFX(_audioClipsSFXRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySFX(_audioClipsSFXRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

}