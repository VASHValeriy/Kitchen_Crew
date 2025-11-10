using UnityEngine;
using UnityEngine.UI;

public class CloseUIBtn : MonoBehaviour {

    [Header("TargetObjects")]
    [SerializeField] private GameObject _targetCloseObject;
    [SerializeField] private GameObject _targetOpenObject;

    [Header("Buttons")]
    [SerializeField] private Button _button;

    private void Awake() {
        if (_button == null)
            _button = GetComponent<Button>();

        if (_button != null) _button.onClick.AddListener(SwitchObjects);
    }


    private void SwitchObjects() {
        if (_targetCloseObject != null)
            _targetCloseObject.SetActive(false);

        if (_targetOpenObject != null)
            _targetOpenObject.SetActive(true);
    }

}