using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject()) //No hay item sobre la mesa
        {
            
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
        else //Hay item sobre la mesa
        {
            
            if (player.HasKitchenObject()) //El jugador tiene algo así que no lo puede recoger
            {                

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) //El jugador lleva un plato
                {
                    //Sí lo puede recoger
                    //Agregamos el ingrediente a la lista (un plato puede llevar varios ingredientes)
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) 
                    {
                        GetKitchenObject().DestroySelf(); //Se borra el item de la mesa
                    } 
                   
                }
                else //El jugador no lleva un plato, pero sí otra cosa
                {
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) //La mesa tiene un plato
                    {                        
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) //Validar ingredientes
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }

            else
            {
                //El jugador no tiene nada, recoge el objeto
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
