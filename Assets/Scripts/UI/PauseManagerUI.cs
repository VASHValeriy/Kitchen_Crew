using System;
using UnityEngine;

public class PauseManagerUI : MonoBehaviour {

    [SerializeField] private GameObject _pauseGameOptionsMenu;

    private bool _isGamePause = false;

    private void OnEnable() {
        if (GameInput.Instance != null) {
            GameInput.Instance.OnGamePauseAction += GameInput_OnGamePauseAction;
        } else {
            GameInput.OnGameInputReady += Subscribe;
        }
    }

    private void OnDisable() {
        if (GameInput.Instance != null)
            GameInput.Instance.OnGamePauseAction -= GameInput_OnGamePauseAction;

        GameInput.OnGameInputReady -= Subscribe;
    }

    private void Subscribe(GameInput input) {
        input.OnGamePauseAction += GameInput_OnGamePauseAction;
    }

    private void GameInput_OnGamePauseAction(object sender, EventArgs e) {
        ToggleGamePause();
    }

    public void ToggleGamePause() {
        _isGamePause = !_isGamePause;
        Time.timeScale = _isGamePause ? 0f : 1f;

        if (_pauseGameOptionsMenu != null) {
            _pauseGameOptionsMenu.SetActive(_isGamePause);
        }
    }

    public bool IsPaused() {
        return _isGamePause;
    }

    public void ResetPause() {
        _isGamePause = false;
        Time.timeScale = 1f;
        if (_pauseGameOptionsMenu != null)
            _pauseGameOptionsMenu.SetActive(false);
    }
}