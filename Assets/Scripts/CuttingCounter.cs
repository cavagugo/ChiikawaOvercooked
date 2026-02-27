using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO cutKichenObjectSO;
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

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().DestroySelf();
            //Llamamos a la clase porque el método es estático
            KitchenObject.SpawnKitchenObject(cutKichenObjectSO, this);

        }
    }
}
