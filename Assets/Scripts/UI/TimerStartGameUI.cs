using TMPro;
using UnityEditor.Search;
using UnityEngine;

public class TimerStartGameUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _timerStartGameUI;

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
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

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}