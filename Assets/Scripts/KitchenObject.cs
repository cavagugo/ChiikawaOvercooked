using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    //Hacer que cambie de parent/mesa. El parametro que recibe es la nueva mesa
    public void SetClearCounter(ClearCounter clearCounter)
    {
        //Vamos a la mesa/padre actual y la limpiamos/liberamos
        if (this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        //Vamos a la nueva mesa y ponemos el objeto que estaba en la mesa anterior
        this.clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("La mesa ya tiene un KitchenObject");
        }
        clearCounter.SetKitchenObject(this);
        //Poner el objeto en dicha mesa (visualmente)
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        //Nos aseguramos que esté bien colocado.
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
