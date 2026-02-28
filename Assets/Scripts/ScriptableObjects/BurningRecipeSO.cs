using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    //Al dejar mucho tiempo input, recibimos output
    public KitchenObjectSO input;
    public KitchenObjectSO output;


    public float burningTimerMax;
}
