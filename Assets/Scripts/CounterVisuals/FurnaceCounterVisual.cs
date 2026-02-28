using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceCounterVisual : MonoBehaviour
{
    [SerializeField] private FurnaceCounter furnaceCounter;
    [SerializeField] private GameObject trayOn;

    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        furnaceCounter.OnStateChanged += FurnaceCounter_OnStateChanged;
        furnaceCounter.OnPlayerGrabbedOrPlacedObject += FurnaceCounter_OnPlayerGrabbedOrPlacedObject;
    }

    private void FurnaceCounter_OnPlayerGrabbedOrPlacedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }

    private void FurnaceCounter_OnStateChanged(object sender, FurnaceCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == FurnaceCounter.State.Baking || e.state == FurnaceCounter.State.Baked;
        trayOn.SetActive(showVisual);
    }
}
