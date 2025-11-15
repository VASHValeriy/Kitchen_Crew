using UnityEngine;

public class GameSceneStart : MonoBehaviour {

    private void Awake() {
        if (SoundManager.Instance == null) {
            GameObject soundManager = Resources.Load<GameObject>("Prefabs/SoundManager");
            Instantiate(soundManager);
        }

        if (GameManager.Instance == null) {
            GameObject gameManager = Resources.Load<GameObject>("Prefabs/GameManager");
            Instantiate(gameManager);
        }

        if (GameInput.Instance == null) {
            GameInput gameInput = Resources.Load<GameInput>("Prefabs/GameInput");
            Instantiate(gameInput);
        }

        // Настраиваем стартовое состояние для GameManager
        if (GameManager.Instance != null) {
            // Если мы в GameScene — сразу запускаем игру
            string activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (activeScene == "GameScene") {
                // Меняем состояние на PauseBeforeStart
                GameManager.Instance.StartGame();
                GameInput gameInput = Resources.Load<GameInput>("Prefabs/GameInput");
                Instantiate(gameInput);
            }
        }

    }


}