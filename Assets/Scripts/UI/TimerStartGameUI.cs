using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class TimerStartGameUI : BaseUI {

    [SerializeField] private TextMeshProUGUI _timerStartGameUI;

    private void OnEnable() {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void OnDisable() {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void Update() {
        _timerStartGameUI.text = Mathf.Ceil(GameManager.Instance.GetTimerToStart()).ToString();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if(GameManager.Instance.isTimerBeforeStartIsActive()) {
            Show();
        } else {
            Hide();
        }
    }
}