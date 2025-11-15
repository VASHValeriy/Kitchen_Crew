using UnityEngine;
using System;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnDropTrash;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnDropTrash?.Invoke(this, EventArgs.Empty);
        }

    }

}