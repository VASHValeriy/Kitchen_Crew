using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGameUIBtn : MonoBehaviour {

    private void Start() {
        Button restBtn = GetComponent<Button>();
        if(restBtn != null) {
            restBtn.onClick.AddListener(RestartGame);
        }
    }

    private void RestartGame() {
        Time.timeScale = 1f;

        GameManager.Instance.RestartGame();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}