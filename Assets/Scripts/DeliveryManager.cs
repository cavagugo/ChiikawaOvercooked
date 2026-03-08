using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; } //Usamos singleton 

    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        //Genera órdenes cada 4 segundos
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            //Limita el némero de órdenes
            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                //Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; ++i) //Pasamos por todas las órdenes
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            bool plateContentsMatchesRecipe = true;

            foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
            {
                //Checando todos los ingredientes de la receta
                bool ingredientFound = false;
                foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                {                    
                    //Checando todos los ingredientes del plato
                    if (plateKitchenObjectSO == recipeKitchenObjectSO)
                    {
                        //El ingrediente coincide
                        ingredientFound = true;
                        break;
                    }
                }
                if (!ingredientFound)
                {
                    //El ingrediente de la receta no se encuentra en el plato
                    plateContentsMatchesRecipe = false; //Si al menos uno no se encuentra, es falso
                }
            }
            
            if (plateContentsMatchesRecipe)
            {
                //El jugador entregó la orden correctamente
                //Debug.Log("El jugador entregó la orden correctamente");

                successfulRecipesAmount++;
                waitingRecipeSOList.RemoveAt(i);
                 
                OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                return;
            }
        }

        //No se encontró coincidencia
        //El jugador entregó una orden INCORRECTA
        //Debug.Log("El jugador entregó una orden INCORRECTA");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
