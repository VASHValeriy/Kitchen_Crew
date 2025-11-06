using UnityEngine;
using UnityEngine.UI;

public class PlatesIconUI : MonoBehaviour {

    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private Transform _iconTemplate;

    private void Awake() {
        _iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        _plateKitchenObject.OnIngridientAdded += _plateKitchenObject_OnIngridientAdded;
    }

    private void _plateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == _iconTemplate) continue;
                Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in _plateKitchenObject.GetKitchenObjectSOList()) {
            Transform iconTransform = Instantiate(_iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
