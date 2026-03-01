using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    //Hacemos el evento del mismo tipo que los argumentos
    public event EventHandler <OnIngredientAddedEventArgs> OnIngredientAdded;

    //Definimos el argumento para pasar los ingredientes
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private KitchenObjectSO glazedCakeKitchenObjectSO;

    private List<KitchenObjectSO> kitchenObjectSOList;
    private bool hasGlazedCake = false;
    private bool hasFruit = false;

    private void Awake()
    {
       kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    //El primer ingrediente debe ser Glazed Cake, para que visualmente, las frutas no estén volando
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (hasGlazedCake) //Los siguientes ingredientes que se agreguen siempre serán fruta
        {
            if (!validKitchenObjectSOList.Contains(kitchenObjectSO))
            {
                //No es un ingrediente válido
                return false;
            }

            if (kitchenObjectSOList.Contains(kitchenObjectSO)) //Ya tiene ese ingrediente (Glazed Cake)
            {
                return false;
            }
            else //Aun no lo tiene (frutas), entonces lo agregamos
            {
                if (!hasFruit) //Si no tiene fruta, le ponemos fruta
                {
                    kitchenObjectSOList.Add(kitchenObjectSO);
                    hasFruit = true;
                    OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                    {
                        kitchenObjectSO = kitchenObjectSO
                    });
                    return true;
                }
                else //Si ya tiene algun tipo de fruta, no se puede agregar más
                {
                    return false;
                }
                
            }
        }
        else //No tiene GlazedCake
        {
            if (kitchenObjectSO == glazedCakeKitchenObjectSO) //El primer ingrediente debe ser Glazed Cake
            {
                kitchenObjectSOList.Add(kitchenObjectSO);

                OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                {
                    kitchenObjectSO = kitchenObjectSO
                });
                hasGlazedCake = true;
                return true;
            }
            else
            {
                return false; //Si intenta agregar fruta antes que la base del pastel (GlazedCake), no podrá hacerlo
            }
        }
        
    } 

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
