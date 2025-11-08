using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundUI : MonoBehaviour {


    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxToggle;



    private void Start() {
        // Настраиваем значения при запуске
        _musicSlider.value = 1f;
        _sfxToggle.value = 1f;

        // Подписываемся на события
        _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        _sfxToggle.onValueChanged.AddListener(ONSfxToggleChanged);

        // Применяем текущие значения слайдеров
        OnMusicValueChanged(_musicSlider.value);
        ONSfxToggleChanged(_sfxToggle.value);
    }

    private void OnMusicValueChanged(float value) {
        SoundManager.Instance.SetMusicVolume(value);
    }

    private void ONSfxToggleChanged(float value) {
        SoundManager.Instance.SetSfxVolume(value);
    }


}