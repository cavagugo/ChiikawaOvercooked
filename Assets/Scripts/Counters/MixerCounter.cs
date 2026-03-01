using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    //Hacemos el evento del mismo tipo que los argumentos
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;    
    //Definimos el argumento para pasar los ingredientes
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }


    public enum State
    {
        Idle,
        Mixing,
        Mixed,
    }



    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList; //Ingredientes válidos (huevo, harina, azúcar)
    [SerializeField] private KitchenObjectSO doughOutputKitchenObjectSO;

    private List<KitchenObjectSO> kitchenObjectSOList; //Ingredientes que el jugador pondrá
    private State state;
    private float mixingTimerMax = 4f;
    private float mixingTimer;


    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();        
    }

    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        if (HasIngredients()) //Verificar el uso de esto por el Idle (no tendría ingredientes)
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Mixing:
                    mixingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = mixingTimer / mixingTimerMax
                    });

                    Debug.Log(mixingTimer);
                    if (mixingTimer > mixingTimerMax) //Si el tiempo de batido supera el tiempo máximo
                    {
                        //La mezcla está lista
                        kitchenObjectSOList.Clear();
                        state = State.Mixed; //Cambiamos al estado final
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state //state de este lado es el privado
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });

                    }

                    break;
                case State.Mixed:

                    break;
            }
        }
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() && state == State.Idle) //El jugador tiene algo Y la batidora está en Idle
        {
            TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO(), player);
        }
        else 
        {
            if (!player.HasKitchenObject() && state == State.Mixed) //Comprobamos si la batidora terminó Y el jugador no tiene nada
            {
                //Ya terminó, lo puede agarrar
                KitchenObject.SpawnKitchenObject(doughOutputKitchenObjectSO, player);
                kitchenObjectSOList.Clear();
                state = State.Idle; //Cambiamos el estado de la batidora
                mixingTimer = 0f; //Reseteamos el tiempo por si acaso

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
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


            if (kitchenObjectSOList.Count >= validKitchenObjectSOList.Count)
            {
                state = State.Mixing;
            }
            return true;

        }
    }

    private bool HasIngredients()
    {
        return kitchenObjectSOList.Count != 0;
    }
}
