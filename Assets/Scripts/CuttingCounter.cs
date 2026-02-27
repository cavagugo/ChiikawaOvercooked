using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        //No hay item sobre la mesa
        if (!HasKitchenObject())
        {
            //El jugador tiene un item 
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //El item se puede cortar, entonces lo poned
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }  
                
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
        //Hay un objeto Y puede ser cortado (para casos donde el jugador ya haya cortado algo e intente cortarlo de nuevo)
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            //Buscamos el output antes de destruir el objeto
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            
            //Llamamos a la clase porque el método es estático
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);

        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        //Regresa true si se puede cortar
        return !(GetOutputForInput(kitchenObjectSO) == null);
    }

    //Busca el ingrediente que debemos cortar/preparar en el arreglo
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
