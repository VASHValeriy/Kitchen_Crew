using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundUI : MonoBehaviour {

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start() {
        SoundManager.Instance.GetVolumeMusicSlider(_musicSlider);
        SoundManager.Instance.GetVolumeSfxSlider(_sfxSlider);

        // Подписываемся на события
        _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        _sfxSlider.onValueChanged.AddListener(ONSfxToggleChanged);

        // Применяем текущие значения слайдеров
        OnMusicValueChanged(_musicSlider.value);
        ONSfxToggleChanged(_sfxSlider.value);
    }

    private void OnMusicValueChanged(float value) {
        SoundManager.Instance.SetMusicVolume(value);
    }

    private void ONSfxToggleChanged(float value) {
        SoundManager.Instance.SetSfxVolume(value);
    }


}