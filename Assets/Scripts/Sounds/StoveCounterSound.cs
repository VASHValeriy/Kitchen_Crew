using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;
    private AudioSource _audioSource;

    private float _warningTimerForSound;
    private bool _isWarningPlaying;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;

        if (SoundManager.Instance != null) {
            _audioSource.volume = SoundManager.Instance.GetSFXVolume();
            SoundManager.Instance.OnSfxVolumeChanged += UpdateSFXVolume;
        }
    }

    private void Update() {
        if (_isWarningPlaying) {
            _warningTimerForSound -= Time.deltaTime;
            if (_warningTimerForSound <= 0f) {
                float warningTimerSoundMax = .2f;
                _warningTimerForSound = warningTimerSoundMax;

                SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
            }
        }
    }

    private void OnDestroy() {
        if (SoundManager.Instance != null) {
            SoundManager.Instance.OnSfxVolumeChanged -= UpdateSFXVolume;
        }

        if (_stoveCounter != null) {
            _stoveCounter.OnStateChanged -= _stoveCounter_OnStateChanged;
        }
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool isFrying = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (isFrying) {
            _audioSource.Play();
        } else {
            _audioSource.Pause();
        }
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnProgressAmount = .5f;
        _isWarningPlaying = _stoveCounter.isFried() && e.progressNormalized >= burnProgressAmount;
    }

    public void UpdateSFXVolume(float newVolume) {
        _audioSource.volume = newVolume;
    }
}