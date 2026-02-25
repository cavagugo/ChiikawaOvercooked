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
            //No hay item sobre la mesa
            if (player.HasKitchenObject())
            {
                //El jugador tiene un item y lo coloca
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //El jugador no tiene nada
            }
        }
        else
        {
            //Hay item sobre la mesa
            if (player.HasKitchenObject())
            {
                //El jugador tiene algo
            }
            else
            {
                //El jugador no tiene nada, recoge el objeto
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
