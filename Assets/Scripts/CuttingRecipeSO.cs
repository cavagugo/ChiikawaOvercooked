using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    //Al colocar input y cortar, recibimos output
    public KitchenObjectSO input;
    public KitchenObjectSO output;


    public int cuttingProgressMax;
}
