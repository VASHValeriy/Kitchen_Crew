using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    public static bool isUIFocused;

    public static event Action<GameInput> OnGameInputReady;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnGamePauseAction;

    private PlayerInputActions _playerInputActions;

    float _targetSpeed;
    float _currentSpeed;
    float _acceleration = 10f;

    private void Awake() {
        Instance = this;

        InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.UI.Disable();

        _playerInputActions.Player.Interact.performed += Interact_performed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        _playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        _playerInputActions.Dispose();
    }

    private void Start() {
        NotifyReadyGame();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnGamePauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 direction = _playerInputActions.Player.Move.ReadValue<Vector2>();
        direction = direction.normalized;
        return direction;
    }

    private void NotifyReadyGame() {
        OnGameInputReady?.Invoke(this);
    }

    public float GetSprintMultiplier() {
        bool isSprinting = _playerInputActions.Player.Sprint.ReadValue<float>() > 0.5f;
            
        _targetSpeed= isSprinting ? 1.5f : 1f;
        _currentSpeed = Mathf.Lerp(_currentSpeed, _targetSpeed, Time.deltaTime * _acceleration);

        return _currentSpeed;
    }

}