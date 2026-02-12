using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IKitchenObjectParent {
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedItem;
    public event EventHandler<OnSelectedCounterChangedEventrArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventrArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private LayerMask _counterLayerMask;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Transform _kitchenObjectHoldPoint;

    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private Vector3 _lastInteractDir;

    [SerializeField] private float _moveSpeed = 10f;
    public float currentSpeedNormalized { get; private set; }

    private bool _isWalking;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("There more than one Player!");
        }
        Instance = this;
    }

    private void Start() {
        _gameInput.OnInteractAction += _gameInput_OnInteractAction;
        _gameInput.OnInteractAlternateAction += _gameInput_OnInteractAlternateAction;
    }

    private void _gameInput_OnInteractAlternateAction(object sender, EventArgs e) {
        if (_selectedCounter != null) {
            _selectedCounter.InteractAlternate(this);
        }
    }

    private void _gameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (!GameManager.Instance.isGamePlaying()) return;

        if (_selectedCounter != null) {
            _selectedCounter.Interact(this);
        }
    }

    void Update() {
        HandleMovement();
        HandleOnInteractions();

    }

    private void HandleOnInteractions() {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero) {
            _lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, interactDistance, _counterLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {
                SetSelectedCounter(baseCounter);
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
        Vector2 direction = _gameInput.GetMovementVectorNormalized();
        float accelerationDirection = _gameInput.GetSprintMultiplier();

        Vector3 moveDir = new Vector3(direction.x, 0f, direction.y);

        // Вариант лучше чем Physics.Raycast, который отслеживает только центр объекта
        float moveDistance = _moveSpeed * accelerationDirection * Time.deltaTime;
        float playerRadius = .7f;
        float playerHieght = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                moveDir = moveDirX;
            } else {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    moveDir = moveDirZ;
                } else {

                }
            }

        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        _isWalking = moveDir != Vector3.zero;

        currentSpeedNormalized = direction.magnitude * accelerationDirection;
        Debug.Log($"currentSpeedNormalized: {currentSpeedNormalized}");

        if (moveDir != Vector3.zero) {
            float rotateSlerpSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSlerpSpeed);
        }
    }

    public bool IsWalking() {
        return _isWalking;
    }

    private void SetSelectedCounter(BaseCounter clearCounter) {
        this._selectedCounter = clearCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventrArgs {
            selectedCounter = _selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() {
        return _kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        _kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickedItem?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject() {
        return _kitchenObject;
    }

    public void ClearKitchenObject() {
        _kitchenObject = null;
    }
    public bool HasKitchenObject() {
        return _kitchenObject != null;
    }
}
