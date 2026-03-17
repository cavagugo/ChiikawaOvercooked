using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class UsagiMainMenu : MonoBehaviour
{
    //public event EventHandler OnEat;

    [SerializeField] private float minSeconds = 5f;
    [SerializeField] private float maxSeconds = 12f;
    [SerializeField] private ParticleSystem foodParticles;
    [SerializeField] private AudioSource munch;
    [SerializeField] private GameObject[] cakeTypes;
    private Animator animator;
    private bool isPlaying = false;
    private float timer;
    private float timerMax = 1.5f;
    private const string EAT = "Eat";

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RandomlyPlayAnimation());
        HideCakeTypes();
    }

    private void Update()
    {
        if (isPlaying)
        {
            timer += Time.deltaTime;
             if (timer > timerMax)
            {
                foodParticles.Play();
                munch.Play();
                timer = 0;
                isPlaying = false;
            }
        }
    }
    IEnumerator RandomlyPlayAnimation()
    {
        while (true) // Loop indefinitely
        {
            // Wait for the current animation to finish, if necessary
            // For simple, short animations like a blink, this might not be needed.

            // Wait a random amount of time before playing the next animation
            float delay = UnityEngine.Random.Range(minSeconds, maxSeconds);
            HideCakeTypes();
            int index = UnityEngine.Random.Range(0, cakeTypes.Length);
            cakeTypes[index].gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);

            isPlaying = true;
            animator.SetTrigger(EAT);
        }
    }

    public void HideCakeTypes()
    {
        foreach (GameObject type in cakeTypes)
        {
            type.gameObject.SetActive(false);
        }
    }
}
