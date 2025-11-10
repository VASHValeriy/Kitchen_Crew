using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {

    private const string GAME_SCENE = "GameScene";

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    private void Awake() {
        _playButton.onClick.AddListener(() => {
            ScenesLoader.LoadScene(GAME_SCENE);
        });
        _quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}