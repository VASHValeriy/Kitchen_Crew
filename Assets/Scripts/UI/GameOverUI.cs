using TMPro;
using UnityEngine;

public class GameOverUI : BaseUI {

    [SerializeField] private TextMeshProUGUI _textRecipeDelivered;

    protected override void Start() {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance == null) return;
        if (this == null) return;

        if (GameManager.Instance.isGameOver()) {
            Show();

            _textRecipeDelivered.text = DeliveryManager.Instance.GetSuccessRecipeEndedAmount().ToString();
        } else {
            Hide();
        }
    }
}