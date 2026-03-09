using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        Instance = this;
        sfxSlider.onValueChanged.AddListener(delegate { SFXValueChangeCheck(); });
        bgmSlider.onValueChanged.AddListener(delegate { BGMValueChangeCheck(); });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    public void SFXValueChangeCheck()
    {
        //Debug.Log(sfxSlider.value);
        SoundManager.Instance.ChangeVolume(sfxSlider.value);
    }

    public void BGMValueChangeCheck()
    {
        //Debug.Log(bgmSlider.value);
        MusicManager.Instance.ChangeVolume(bgmSlider.value);
    }

    public void Show()
    {
        UpdateSliders();
        gameObject.SetActive(true);        
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateSliders()
    {
        sfxSlider.value = SoundManager.Instance.GetVolume();
        bgmSlider.value = MusicManager.Instance.GetVolume();
    }
}
