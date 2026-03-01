using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerCounter : BaseCounter
{

    //Hacemos el evento del mismo tipo que los argumentos
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

    //Definimos el argumento para pasar los ingredientes
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList; //Ingredientes válidos (huevo, harina, azúcar)
    [SerializeField] private KitchenObjectSO doughOutputKitchenObjectSO;

    private List<KitchenObjectSO> kitchenObjectSOList; //Ingredientes que el jugador pondrá
    private bool isMixing;


    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
        isMixing = false;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) //El jugador tiene algo
        {
            TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO(), player);
        }
        else //No tiene nada
        {            
            if (isMixing) //Comprobamos que la batidora terminó
            {
                //No ha terminado
                //No lo puede agarrar

                //Ya terminó, lo puede agarrar
                KitchenObject.SpawnKitchenObject(doughOutputKitchenObjectSO, player);
                kitchenObjectSOList.Clear();
                isMixing = false;
            }
            else
            {
                
            }

        }
        
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO, Player player)
    {

        if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //No es un ingrediente válido
            return false;
        }

        if (kitchenObjectSOList.Contains(kitchenObjectSO)) //Ya tiene ese ingrediente
        {
            return false;
        }
        else //Aun no lo tiene, entonces lo agregamos
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            player.GetKitchenObject().DestroySelf();
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });

            int maxIngredientsPerRecipe = 3;

            if (kitchenObjectSOList.Count >= maxIngredientsPerRecipe)
            {
                isMixing = true;
            }
            return true;

        }
    }
}
