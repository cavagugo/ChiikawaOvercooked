using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlazingCounter : BaseCounter, IHasProgress
{

    //Hacer referencia a los argumentos de la interface
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler OnGlaze;
    public static event EventHandler OnAnyGlaze;

    new public static void ResetStaticData()
    {
        OnAnyGlaze = null;
    }


    [SerializeField] private GlazingRecipeSO[] glazingRecipeSOArray;

    private int glazingProgress;
    public override void Interact(Player player)
    {
        //No hay item sobre la mesa
        if (!HasKitchenObject())
        {
            //El jugador tiene un item 
            if (player.HasKitchenObject())
            {
                //El item es la masa/pan, entonces lo pone
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    glazingProgress = 0;

                    //Evento que actualiza la barra de progreso en la UI
                    GlazingRecipeSO glazingRecipeSO = GetGlazingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        //Casteamos el primer valor a float para que el resultado no quede en un int
                        progressNormalized = (float)glazingProgress / glazingRecipeSO.glazingProgressMax
                    });
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

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) //El jugador lleva un plato
                {
                    //Agregamos el ingrediente a la lista (un plato puede llevar varios ingredientes)
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf(); //Se borra el item de la mesa
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

    public override void InteractAlternate(Player player)
    {
        //Hay un objeto Y puede ser cortado (para casos donde el jugador ya haya cortado algo e intente cortarlo de nuevo)
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            glazingProgress++;

            OnGlaze?.Invoke(this, EventArgs.Empty);
            OnAnyGlaze?.Invoke(this, EventArgs.Empty);
            GlazingRecipeSO glazingRecipeSO = GetGlazingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                //Casteamos el primer valor a float para que el resultado no quede en un int
                progressNormalized = (float)glazingProgress / glazingRecipeSO.glazingProgressMax
            });


            //El ingrediente se corta hasta que el progreso esté completo
            if (glazingProgress >= glazingRecipeSO.glazingProgressMax)
            {
                //Buscamos el output antes de destruir el objeto
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                //Llamamos a la clase porque el método es estático
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        GlazingRecipeSO glazingRecipeSO = GetGlazingRecipeSOWithInput(inputKitchenObjectSO);
        //Regresa true si se puede glasear
        return (glazingRecipeSO != null);
    }

    //Busca el ingrediente que debemos cortar/preparar en el arreglo
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        GlazingRecipeSO glazingRecipeSO = GetGlazingRecipeSOWithInput(inputKitchenObjectSO);
        if (glazingRecipeSO != null)
        {
            return glazingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private GlazingRecipeSO GetGlazingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (GlazingRecipeSO glazingRecipeSO in glazingRecipeSOArray)
        {
            if (glazingRecipeSO.input == inputKitchenObjectSO)
            {
                return glazingRecipeSO;
            }
        }
        return null;
    }
}
