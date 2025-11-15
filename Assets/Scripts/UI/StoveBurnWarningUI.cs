using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour {

    [SerializeField] private StoveCounter _stoveCounter;

    private void Start() {
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;

        Hide();
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnProgressAmount = .5f;
        bool warning = _stoveCounter.isFried() && e.progressNormalized >= burnProgressAmount;
        
        if(warning) {
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