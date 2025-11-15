using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicSliderContorollerUI : MonoBehaviour {

    public static MusicSliderContorollerUI Instance { get; private set; }
    public bool IsDragging => _isDragging;

    [SerializeField] private Slider _musicSliderLength;
    private AudioSource _audioSource;

    private bool _isDragging = false;

    private void Awake() {
        Instance = this;
    }

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
            // Безопасно обновляем позицию после отпускания слайдера
            float newTime = Mathf.Clamp(_musicSliderLength.value, 0f, _audioSource.clip.length - 0.01f);
            _audioSource.time = newTime;

            // Если музыка была на паузе — запускаем
            if (!_audioSource.isPlaying) {
                _audioSource.Play();
            }
        }
    }
}