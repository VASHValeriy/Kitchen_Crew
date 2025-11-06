using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particalesGameObject;

    private void Start() {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        if(e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried) {
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        _stoveOnGameObject.SetActive(true);
        _particalesGameObject.SetActive(true);
    }
    private void Hide() {
        _stoveOnGameObject.SetActive(false);
        _particalesGameObject.SetActive(false);
    }
}