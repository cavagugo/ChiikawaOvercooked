using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    //Para la animación del container
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        //El jugador no tiene nada y le da un objeto
        if (!player.HasKitchenObject())
        {
            //Instanciamos el prefab y se lo da al jugador
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.localPosition = Vector3.zero;
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            

            //Se llama al evento
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }

    }

    
}
