using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {


    [Header("Main Menu Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;

    [Header("Canvas Links")]
    [SerializeField] private GameObject _optionsMenuCanvas;
    [SerializeField] private GameObject _closeMainMenuCanvas;

    private void Awake() {
        _playButton.onClick.AddListener(() => {
            GameManager.Instance.StartGame();
        });
        _optionsButton.onClick.AddListener((ToggleOptionsMenu));
    }

    private void ToggleOptionsMenu() {
        bool isActive = _optionsMenuCanvas.activeSelf;

        _optionsMenuCanvas.SetActive(!isActive);
        _closeMainMenuCanvas.SetActive(isActive);
    }

}