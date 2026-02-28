using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCounter : BaseCounter
{
    [SerializeField] private BakingRecipeRecipeSO[] bakingRecipeRecipeSOArray;

    public override void Interact(Player player)
    {
        
        if (!HasKitchenObject()) //No hay item dentro del horno
        {
            //El jugador tiene un item 
            if (player.HasKitchenObject())
            {
                //El item se puede hornear, entonces lo pone
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    
                }

            }
            else
            {
                //El jugador no tiene nada
            }
        }
        else //Hay item dentro del horno
        {
            
            if (player.HasKitchenObject()) //El jugador tiene algo
            {
                //No puede sacar el objeto
            }
            else
            {
                //El jugador no tiene nada, saca el objeto del horno
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BakingRecipeRecipeSO bakingRecipeSO = GetBakingRecipeSOWithInput(inputKitchenObjectSO);
        //Regresa true si se puede cortar
        return (bakingRecipeSO != null);
    }

    //Busca el ingrediente que debemos cortar/preparar en el arreglo
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BakingRecipeRecipeSO bakingRecipeSO = GetBakingRecipeSOWithInput(inputKitchenObjectSO);
        if (bakingRecipeSO != null)
        {
            return bakingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private BakingRecipeRecipeSO GetBakingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BakingRecipeRecipeSO bakingRecipeSO in bakingRecipeRecipeSOArray)
        {
            if (bakingRecipeSO.input == inputKitchenObjectSO)
            {
                return bakingRecipeSO;
            }
        }
        return null;
    }

}
