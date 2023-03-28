using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //柜台上没东西，玩家手上有东西
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //柜台上没东西，玩家手上没东西
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //柜台上有东西，玩家手上有东西
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //玩家持有盘子
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }

            else
            {
                //柜台上有东西，玩家手上没东西
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }
        }

    }
}
