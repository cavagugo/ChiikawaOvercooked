using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    [SerializeField] private float completeAnimDuration = 0.5f;

    // Espejo de la lista de espera, en orden de llegada
    private List<(RecipeSO recipe, Transform card)> _spawnedCards = new();

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += OnSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += OnCompleted;
    }

    private void OnSpawned(object sender, System.EventArgs e)
    {
        var list = DeliveryManager.Instance.GetWaitingRecipeSOList();
        RecipeSO newRecipe = list[^1];

        Transform card = Instantiate(recipeTemplate, container);
        card.gameObject.SetActive(true);
        card.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(newRecipe);
        card.GetComponent<Animator>().SetTrigger("Spawn");

        _spawnedCards.Add((newRecipe, card));
    }

    private void OnCompleted(object sender, int completedIndex)
    {
        if (completedIndex < 0 || completedIndex >= _spawnedCards.Count) return;

        Transform card = _spawnedCards[completedIndex].card;
        _spawnedCards.RemoveAt(completedIndex);

        StartCoroutine(AnimateThenDestroy(card));
    }

    private IEnumerator AnimateThenDestroy(Transform card)
    {
        card.GetComponent<Animator>().SetTrigger("Complete");
        yield return new WaitForSeconds(completeAnimDuration);
        Destroy(card.gameObject);
    }
}