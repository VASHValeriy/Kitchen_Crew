using UnityEngine;

public class ClearCounter : BaseCounter {

    [SerializeField] private KitchenObjectSO _kitchenObjectSO;
    private GameObject _selectedCounter;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                
            }
        } else {
            if (player.HasKitchenObject()) {
                // У игрока что-то в руках
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    //У игрока в руках тарелка
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    //У игрока НЕТ тарелки, но есть другая еда
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        //Counter держит тарелку
                        if (plateKitchenObject.TryAddIngridient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}