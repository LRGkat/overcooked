using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static event EventHandler<OnObjectDeliveriedEventArgs> OnObjectDeliveried;

    public class OnObjectDeliveriedEventArgs : EventArgs
    {
        public bool isSuccess;
        public Transform deliveryCounterTransform;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                bool isSuccess = DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                OnObjectDeliveried?.Invoke(this,new OnObjectDeliveriedEventArgs
                {
                    isSuccess =  isSuccess,
                    deliveryCounterTransform = this.transform
                });
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
    
}
