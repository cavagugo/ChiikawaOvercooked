using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BakingRecipeSO : ScriptableObject
{
    //Al meter input en el horno y esperar un tiempo, recibimos output
    public KitchenObjectSO input;
    public KitchenObjectSO output;


    public float bakingTimerMax;
}
