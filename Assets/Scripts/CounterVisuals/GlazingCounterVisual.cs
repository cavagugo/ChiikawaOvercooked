using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlazingCounterVisual : MonoBehaviour
{
    private const string GLAZE = "Glaze";
    [SerializeField] private GlazingCounter glazingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        glazingCounter.OnGlaze += GlazingCounter_OnGlaze;
    }

    private void GlazingCounter_OnGlaze(object sender, System.EventArgs e)
    {
        animator.SetTrigger(GLAZE);
    }
}
