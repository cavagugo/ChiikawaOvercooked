using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Baking,
        Baked,
        Burnt,
    }
    [SerializeField] private BakingRecipeSO[] bakingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float bakingTimer;
    private BakingRecipeSO bakingRecipeSO;
    private float burningTimer;

    private BurningRecipeSO burningRecipeSO;


    private void Start()
    {
        state = State.Idle; //Inicializamos el estado
    }
    private void Update()
    {
        //Si el horno tiene masa adentro
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Baking:
                    bakingTimer += Time.deltaTime;

                    if (bakingTimer > bakingRecipeSO.bakingTimerMax) //Si el tiempo de horneado es más grande que el tiempo máximo
                    {
                        //La masa está horneada                        
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(bakingRecipeSO.output, this);  //Reemplazamos la masa cruda por masa horneada

                        Debug.Log("Masa horneada");
                        
                        state = State.Baked;
                        burningTimer = 0f; //Inicializamos/resetamos el timer
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Baked:
                    burningTimer += Time.deltaTime;

                    if (burningTimer > burningRecipeSO.burningTimerMax) //Se empieza a quemar
                    {
                        //La masa está quemada                     
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);  //Reemplazamos la masa hornada por la masa quemada

                        Debug.Log("Masa quemada!");
                        state = State.Burnt;
                    }
                    break;
                case State.Burnt:
                    break;
            }
            Debug.Log(state);
        }            
    }
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
                    bakingRecipeSO = GetBakingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Baking; //Cambiamos el estado del horno
                    bakingTimer = 0f; //Reseteamos el tiempo por si acaso
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
                state = State.Idle; //Reseteamos el estado del horno
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BakingRecipeSO bakingRecipeSO = GetBakingRecipeSOWithInput(inputKitchenObjectSO);
        //Regresa true si se puede cortar
        return (bakingRecipeSO != null);
    }

    //Busca el ingrediente que debemos cortar/preparar en el arreglo
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        BakingRecipeSO bakingRecipeSO = GetBakingRecipeSOWithInput(inputKitchenObjectSO);
        if (bakingRecipeSO != null)
        {
            return bakingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private BakingRecipeSO GetBakingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BakingRecipeSO bakingRecipeSO in bakingRecipeSOArray)
        {
            if (bakingRecipeSO.input == inputKitchenObjectSO)
            {
                return bakingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

}
