using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCounterVisual : MonoBehaviour
{
    [SerializeField] private FurnaceCounter furnaceCounter;
    [SerializeField] private GameObject trayOn;

    private void Start()
    {
        furnaceCounter.OnStateChanged += FurnaceCounter_OnStateChanged;
    }

    private void FurnaceCounter_OnStateChanged(object sender, FurnaceCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == FurnaceCounter.State.Baking || e.state == FurnaceCounter.State.Baked;
        trayOn.SetActive(showVisual);
    }
}
