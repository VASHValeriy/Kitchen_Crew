using UnityEngine;
using UnityEngine.UI;

public class BaseCloseUIBtn : MonoBehaviour
{

    [Header("Target Objects")]
    [SerializeField] protected GameObject _targetCloseObject;
    [SerializeField] protected GameObject _targetOpenObject;

    [Header("Button")]
    [SerializeField] private Button _button;

    protected virtual void Awake() {
        if (_button == null)
            _button = GetComponent<Button>();

        if (_button != null)
            _button.onClick.AddListener(SwitchObjects);
    }

    protected virtual void SwitchObjects() {
        if (_targetCloseObject != null)
            _targetCloseObject.SetActive(false);

        if (_targetOpenObject != null)
            _targetOpenObject.SetActive(true);
    }

}