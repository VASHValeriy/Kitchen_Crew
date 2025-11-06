using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour {

    private const string CUT = "Cut";

    [SerializeField] CuttingCounter _cuttingCounter;
    [SerializeField] private GameObject _particlesGameObject;

    private Animator _animator;
    private bool _isCutPlaying = false;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _cuttingCounter.OnCut += _cuttingCounter_OnCut;
        Hide();
    }

    private void _cuttingCounter_OnCut(object sender, System.EventArgs e) {
        _animator.SetTrigger(CUT);
        Show();
    }
    public void HideParticles() {
        Hide();
    }

    private void Show() {
        _particlesGameObject.SetActive(true);
    }
    private void Hide() {
        _particlesGameObject.SetActive(false);
    }
}