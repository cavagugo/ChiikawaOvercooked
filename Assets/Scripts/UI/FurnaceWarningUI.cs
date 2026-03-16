using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceWarningUI : MonoBehaviour
{
    [SerializeField] private FurnaceCounter furnaceCounter;

    private void Start()
    {
        furnaceCounter.OnProgressChanged += FurnaceCounter_OnProgressChanged;
        Hide();
    }

    private void FurnaceCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = furnaceCounter.IsBaked() && e.progressNormalized >= burnShowProgressAmount;

        if (show)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
