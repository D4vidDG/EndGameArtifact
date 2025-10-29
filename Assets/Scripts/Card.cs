using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] Image[] rarityFrames;
    [SerializeField] Image[] heroImages;
    [SerializeField] CardFrameByRarity[] cardFrames;
    [SerializeField] RectTransform selectedPanel;
    [SerializeField] Button useButton;
    [SerializeField] Button infoButton;
    [SerializeField] Button removeButton;

    Dictionary<HeroRarity, Sprite> cardFrameByRarity;
    Hero hero;

    HeroInformationPanel heroInformationPanel;

    public static Action<Card> OnUseButtonClicked;
    public static Action<Card> OnRemoveButtonClicked;

    void Awake()
    {
        cardFrameByRarity = new();
        foreach (HeroRarity rarity in Enum.GetValues(typeof(HeroRarity)))
        {
            cardFrameByRarity.Add(rarity, null);
        }

        foreach (CardFrameByRarity item in cardFrames)
        {
            if (cardFrameByRarity[item.heroRarity] == null)
            {
                cardFrameByRarity[item.heroRarity] = item.frame;
            }
        }

        heroInformationPanel = FindObjectOfType<HeroInformationPanel>(includeInactive: true);
    }

    void OnEnable()
    {
        infoButton.onClick.AddListener(OnInfoClicked);
        useButton.onClick.AddListener(OnUseClicked);
        removeButton.onClick.AddListener(OnRemoveClicked);
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
            Sprite frame = cardFrameByRarity[hero.GetRarity()];
            if (frame != null) image.sprite = frame;
        }

        foreach (Image image in heroImages)
        {

            image.sprite = hero.GetPortrait();
        }

        this.hero = hero;
    }

    public void OnSelect(BaseEventData eventData)
    {
        EnableSelectedPanel(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
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

    [Serializable]
    public class CardFrameByRarity
    {
        public Sprite frame;
        public HeroRarity heroRarity;
    }
}
