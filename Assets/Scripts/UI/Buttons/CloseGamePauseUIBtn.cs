using UnityEngine;
using UnityEngine.UI;

public class CloseGamePauseUIBtn : BaseCloseUIBtn {

    [SerializeField] private PauseManagerUI _pauseManagerUI;

    protected override void SwitchObjects() {
        // Сначала стандартное переключение
        base.SwitchObjects();

        // Дополнительно логика паузы
        if (_pauseManagerUI != null && _pauseManagerUI.IsPaused()) {
            _pauseManagerUI.ToggleGamePause();
        }
    }
}