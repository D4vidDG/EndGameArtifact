using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] Image[] rarityFrames;
    [SerializeField] Image[] heroImages;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] UISettings uiSettings;
    [SerializeField] RectTransform selectedPanel;
    [SerializeField] Button useButton;
    [SerializeField] Button infoButton;
    [SerializeField] Button removeButton;

    Hero hero;

    HeroInformationPanel heroInformationPanel;

    public static Action<Card> OnUseButtonClicked;
    public static Action<Card> OnRemoveButtonClicked;

    void Awake()
    {
        heroInformationPanel = FindObjectOfType<HeroInformationPanel>(includeInactive: true);
    }

    void OnEnable()
    {
        infoButton.onClick.AddListener(OnInfoClicked);
        useButton.onClick.AddListener(OnUseClicked);
        removeButton.onClick.AddListener(OnRemoveClicked);
        level.transform.parent.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (infoButton != null) infoButton.onClick.RemoveListener(OnInfoClicked);
        if (useButton != null) useButton.onClick.RemoveListener(OnUseClicked);
        if (removeButton != null) removeButton.onClick.RemoveListener(OnRemoveClicked);
        if (selectedPanel != null) EnableSelectedPanel(false);
    }

    public Hero GetHero()
    {
        return hero;
    }

    public void SetHero(Hero hero)
    {
        foreach (Image image in rarityFrames)
        {
            Sprite frame = uiSettings.GetUIInfo(hero.GetRarity()).cardFrame;
            if (frame != null) image.sprite = frame;
        }

        foreach (Image image in heroImages)
        {
            image.sprite = hero.GetPortrait();
        }

        level.text = hero.GetLevel().ToString();

        this.hero = hero;
    }

    public void OnSelect(BaseEventData eventData)
    {
        level.transform.parent.gameObject.SetActive(false);
        EnableSelectedPanel(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        level.transform.parent.gameObject.SetActive(true);
        EnableSelectedPanel(false);
    }

    private void EnableSelectedPanel(bool enable)
    {
        selectedPanel.gameObject.SetActive(enable);
    }

    private void OnInfoClicked()
    {
        heroInformationPanel.SetHero(hero);
        heroInformationPanel.gameObject.SetActive(true);
        EnableSelectedPanel(false);
    }

    private void OnUseClicked()
    {
        EnableSelectedPanel(false);
        OnUseButtonClicked.Invoke(this);

    }
    private void OnRemoveClicked()
    {
        EnableSelectedPanel(false);
        OnRemoveButtonClicked.Invoke(this);
    }
}
