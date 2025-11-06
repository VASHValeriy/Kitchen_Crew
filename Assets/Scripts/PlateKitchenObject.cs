using UnityEngine;
using System.Collections.Generic;
using System;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
    public class OnIngridientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList; 

    private List<KitchenObjectSO> _kitchenObjectSOList;

    private void Awake() {
        _kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngridient(KitchenObjectSO kitchenObjectSO) {
        if(!_validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }
        if(_kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false; 
        } else {
            _kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return _kitchenObjectSOList;
    }

}