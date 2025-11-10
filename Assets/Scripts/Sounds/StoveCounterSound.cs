using Autodesk.Fbx;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;

        if(SoundManager.Instance != null) {
            _audioSource.volume = SoundManager.Instance.GetSFXVolume();
            SoundManager.Instance.OnSfxVolumeChanged += UpdateSFXVolume;
        }
    }

    private void OnDestroy() {
        if(SoundManager.Instance != null) {
            SoundManager.Instance.OnSfxVolumeChanged -= UpdateSFXVolume;
        }

        if(_stoveCounter != null) {
            _stoveCounter.OnStateChanged -= _stoveCounter_OnStateChanged;
        }
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool isFrying = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(isFrying) {
            _audioSource.Play();
        } else {
            _audioSource.Pause();
        }
    }

    public void UpdateSFXVolume(float newVolume) {
        _audioSource.volume = newVolume;
    }
}