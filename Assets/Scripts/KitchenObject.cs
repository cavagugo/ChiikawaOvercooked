using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    //Hacer que cambie de parent (jugador o mesa). El parametro que recibe es el nuevo padre
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //Vamos al padre actual y la limpiamos/liberamos
        if (this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        //Vamos al nuevo padre y ponemos el objeto que estaba en el padre anterior
        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("IKitchenObjectParent ya tiene un KitchenObject");
        }
        kitchenObjectParent.SetKitchenObject(this);
        //Poner el objeto en dicho padre (visualmente)
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        //Nos aseguramos que esté bien colocado.
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.localPosition = Vector3.zero;
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);

        return kitchenObject;
    }
}
