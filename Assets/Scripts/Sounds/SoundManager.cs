using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    [Header("Audio Source")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClipsRerfsSO _audioClipsSFXRefsSO;
    public AudioSource MusicSource => _musicSource;

    [Header("Music Settings")]
    public AudioClip mainMenuMusic;      // Полный трек
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
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        Player.Instance.OnPickedItem += Player_OnPickedItem;

        CuttingCounter.OnEveryKnifeSound += CuttingCounter_OnEveryKnifeSound;
        BaseCounter.OnObjectPlacement += BaseCounter_OnObjectPlacement;
        TrashCounter.OnDropTrash += TrashCounter_OnDropTrash;

        PlayMusic(mainMenuMusic);
    }

    private void TrashCounter_OnDropTrash(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySFX(_audioClipsSFXRefsSO.trash, trashCounter.transform.position);
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

    private void Update() {
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

    private void PlaySFX(AudioClip[] clipArray, Vector3 position, float volume = 0.5f ) {
        if (clipArray == null || clipArray.Length == 0) return;
        _sfxSource.transform.position = position;
        _sfxSource.PlayOneShot(clipArray[Random.Range(0, clipArray.Length)], volume * _sfxSource.volume);
    }

    public void PlayStepsOfPlayers(Vector3 position, float volume = 0.5f) {
        PlaySFX(_audioClipsSFXRefsSO.footStep, position, volume * _sfxSource.volume);        
        } 

    public void SetMusicVolume(float volume) {
        _musicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSfxVolume(float volume) {
        _sfxSource.volume = Mathf.Clamp01(volume);
    }

    public void StopMusic() => _musicSource.Stop();

}