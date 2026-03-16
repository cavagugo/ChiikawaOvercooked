using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceFlashingBarUI : MonoBehaviour
{
    [SerializeField] private FurnaceCounter furnaceCounter;
    private Animator animator;
    private const string IS_FLASHING = "IsFlashing";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        furnaceCounter.OnProgressChanged += FurnaceCounter_OnProgressChanged;

        animator.SetBool(IS_FLASHING, false);
    }

    private void FurnaceCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = furnaceCounter.IsBaked() && e.progressNormalized >= burnShowProgressAmount;
        animator.SetBool(IS_FLASHING, show);
    }
}
