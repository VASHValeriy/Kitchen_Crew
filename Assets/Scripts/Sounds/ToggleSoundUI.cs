using UnityEngine;
using UnityEngine.UI;

public class ToggleSoundUI : MonoBehaviour {


    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Toggle _sfxToggle;


    private void Start() {
        // Настраиваем значения при запуске
        _musicSlider.value = 1f;
        _sfxToggle.isOn = true;

        // Подписываемся на события
        _musicSlider.onValueChanged.AddListener(OnMusicValueChanged);
        _sfxToggle.onValueChanged.AddListener(ONSfxToggleChanged);

    }

    private void OnMusicValueChanged(float value) {
        SoundManager.Instance.SetMusicVolume(value);
    }

    private void ONSfxToggleChanged(bool isOn) {
        SoundManager.Instance.SetSfxVolume(isOn ? 1f : 0f);
    }


}