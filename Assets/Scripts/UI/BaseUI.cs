using UnityEngine;

public class BaseUI : MonoBehaviour {

    protected virtual void Start() {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }
    protected virtual void OnDestroy() {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) { }

    protected void Show() => gameObject.SetActive(true);
    protected void Hide() => gameObject.SetActive(false);
}