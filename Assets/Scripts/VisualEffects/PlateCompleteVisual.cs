using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {

    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private PlateKitchenObject _plateKithcenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSOGameObjectList; 

    private void Start() {
        _plateKithcenObject.OnIngridientAdded += _plateKithcenObject_OnIngridientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList) {
                kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void _plateKithcenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e) {
        foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in _kitchenObjectSOGameObjectList) {
            if(kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO) {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}