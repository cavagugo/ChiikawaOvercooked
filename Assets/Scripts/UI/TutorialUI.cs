using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; private set; }
    public event EventHandler OnClosedTutorial;



    [SerializeField] private TextMeshProUGUI keyMoveUpText;
    [SerializeField] private TextMeshProUGUI keyMoveDownText;
    [SerializeField] private TextMeshProUGUI keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI keyMoveRightText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractAltText;
    [SerializeField] private TextMeshProUGUI keyPauseText;
    [SerializeField] private TextMeshProUGUI keyInteractGamepadText;
    [SerializeField] private TextMeshProUGUI keyInteractAltGamepadText;
    [SerializeField] private TextMeshProUGUI keyPauseGamepadText;


    [SerializeField] private GameObject firstPanel;
    [SerializeField] private GameObject lastPanel;
    [SerializeField] private GameObject[] panelsArray;
    private GameObject currentPanel;
    private int currentPanelIndex = 0;

    [SerializeField] private Button next;
    [SerializeField] private Button previous;
    [SerializeField] private Button close;


    private void Awake()
    {
        Instance = this;
        next.onClick.AddListener(NextPanel);
        previous.onClick.AddListener(PreviousPanel);
        close.onClick.AddListener(CloseTutorial);

    }
    private void Start()
    {
        GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        currentPanel = firstPanel;
        next.Select();
        UpdatePanels();
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        keyInteractGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        keyInteractAltGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        keyPauseGamepadText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    private void UpdatePanels()
    {
        currentPanel = panelsArray[currentPanelIndex];
        next.gameObject.SetActive(true);
        previous.gameObject.SetActive(true);

        foreach (GameObject panel in panelsArray) panel.SetActive(false);


        currentPanel.SetActive(true);


        //Manejar botones dependiendo de si es el panel inicial o final
        if (currentPanel == firstPanel)
        {
            previous.gameObject.SetActive(false);
        }
        if (currentPanel == lastPanel)
        {
            next.gameObject.SetActive(false);
        }

        if (!next.gameObject.activeSelf)
        {
            previous.Select();
        }
        if (!previous.gameObject.activeSelf)
        {
            next.Select();
        }
    }

    private void NextPanel()
    {
        if (currentPanelIndex <= panelsArray.Length-1)
        {
            currentPanelIndex++;
            UpdatePanels();
        }
        //Debug.Log(currentPanelIndex);
    }
    private void PreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            UpdatePanels();
        }
        //Debug.Log(currentPanelIndex);
    }

    private void CloseTutorial()
    {
        OnClosedTutorial?.Invoke(this, EventArgs.Empty);
        Hide();
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
