using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicSliderContorollerUI : MonoBehaviour {

    [SerializeField] private Slider _musicSliderLength;
    private AudioSource _audioSource;

    private bool _isDragging = false;
    public static bool IsUIActive;


    private IEnumerator Start() {
        yield return new WaitUntil(() => SoundManager.Instance != null && SoundManager.Instance.MusicSource != null);

        _audioSource = SoundManager.Instance.MusicSource;

        yield return new WaitUntil(() => _audioSource.clip != null);

        _musicSliderLength.minValue = 0f;
        _musicSliderLength.maxValue = _audioSource.clip.length;


        _musicSliderLength.onValueChanged.AddListener(delegate {
            if (_isDragging) _audioSource.time = _musicSliderLength.value;
        });

        _musicSliderLength.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void Update() {
        if (_audioSource == null || _musicSliderLength == null) return;

        if (!_isDragging && _audioSource.isPlaying) {
            _musicSliderLength.value = _audioSource.time;
        }
    }

    private void OnSliderValueChanged(float value) {
        if (_audioSource != null && _isDragging) {
            if (!_audioSource.isPlaying) {
                _audioSource.Play();
            }
            _audioSource.time = value;
        }
    }

    public void StartDrag() {
        _isDragging = true;
    }

    public void EndDrag() {
        _isDragging = false;

        if (_audioSource != null) {
            if (!_audioSource.isPlaying) _audioSource.Play();
            _audioSource.time = _musicSliderLength.value;
        }
    }
}