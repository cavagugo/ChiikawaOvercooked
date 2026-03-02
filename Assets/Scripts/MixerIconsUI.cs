using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixerIconsUI : MonoBehaviour
{
    [SerializeField] private MixerCounter mixerCounter;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Transform iconDone;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
        iconDone.gameObject.SetActive(false);
    }

    private void Start()
    {
        mixerCounter.OnIngredientAdded += MixerCounter_OnIngredientAdded;
        mixerCounter.OnStateChanged += MixerCounter_OnStateChanged;
    }

    private void MixerCounter_OnStateChanged(object sender, MixerCounter.OnStateChangedEventArgs e)
    {
        UpdateVisual();
        if (e.state == MixerCounter.State.Mixed)
        {
            iconDone.gameObject.SetActive(true);
        }

        if (e.state == MixerCounter.State.Idle)
        {
            iconDone.gameObject.SetActive(false);
        }
    }

    private void MixerCounter_OnIngredientAdded(object sender, MixerCounter.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        //Quita iconos viejos excepto la plantilla
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        //Agrega iconos nuevos
        foreach (KitchenObjectSO kitchenObjectSO in mixerCounter.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSOImage(kitchenObjectSO);
        }
    }
}