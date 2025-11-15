using UnityEngine;

public class BurnWarningProgressBarFlashUI : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;

    private const string IS_WARNING = "IsWarning";

    private Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;

        _animator.SetBool(IS_WARNING, false);
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnProgressAmount = .5f;
        bool warning = _stoveCounter.isFried() && e.progressNormalized >= burnProgressAmount;

        _animator.SetBool(IS_WARNING, warning);

    }
}
