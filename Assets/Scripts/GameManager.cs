using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    public enum State {
        MainMenuScene,
        PauseBeforeStart,
        TimerToStart,
        GamePlay,
        GameOver,
    }

    public State _state;

    private float _timeBeforeStart = .1f;
    private float _timeToStart = 3f;
    private float _itsTimeToPlay;
    private float _itsTimeToPlayMax = 50f;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _state = State.MainMenuScene;
    }

    private void Update() {
        if (_state == State.MainMenuScene) return;

        switch (_state) {
            case State.MainMenuScene:
                break;
            case State.PauseBeforeStart:
                _timeBeforeStart -= Time.deltaTime;
                if (_timeBeforeStart < 0) {
                    _state = State.TimerToStart;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.TimerToStart:
                _timeToStart -= Time.deltaTime;
                if (_timeToStart < 0) {
                    _state = State.GamePlay;
                    _itsTimeToPlay = _itsTimeToPlayMax;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlay:
                _itsTimeToPlay -= Time.deltaTime;
                if (_itsTimeToPlay < 0) {
                    _state = State.GameOver;

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public void StartGame() {
        if (_state == State.MainMenuScene) {
            _state = State.PauseBeforeStart;

            _timeBeforeStart = 0.1f;
            _timeToStart = 3f;
            _itsTimeToPlay = _itsTimeToPlayMax;

            OnStateChanged?.Invoke(this, EventArgs.Empty);

            ScenesLoader.LoadScene(ScenesLoader.Scenes.GameScene);
        }
    }

    public bool isGamePlaying() {
        return _state == State.GamePlay;
    }

    public bool isTimerBeforeStartIsActive() {
        return _state == State.TimerToStart;
    }

    public float GetTimerToStart() {
        return _timeToStart;
    }

    public bool isGameOver() {
        return _state == State.GameOver;
    }

    public float GetitsTimeToPlayNormalized() {
        return 1 - (_itsTimeToPlay / _itsTimeToPlayMax);
    }

    public void ReturnToMenu() {
        _state = State.MainMenuScene;

        // —брос таймера
        _timeBeforeStart = 0.1f;
        _timeToStart = 3f;
        _itsTimeToPlay = _itsTimeToPlayMax;

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RestartGame() {
        _state = State.PauseBeforeStart;

        // —брос таймера
        _timeBeforeStart = 0.1f;
        _timeToStart = 3f;
        _itsTimeToPlay = _itsTimeToPlayMax;

        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
}