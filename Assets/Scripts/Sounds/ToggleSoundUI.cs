using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundUI : MonoBehaviour {

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start() {
        // Настраиваем значения при запуске
        _musicSlider.value = 1f;
        _sfxSlider.value = 1f;

        SoundManager.Instance.GetVolumeMusicSlider(_musicSlider);

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