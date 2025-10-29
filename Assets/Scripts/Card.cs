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

    Dictionary<HeroRarity, Sprite> cardFrameByRarity;
    Hero hero;
    RectTransform rectTransform;

    public RectTransform RectTransform => rectTransform;

    HeroInformationPanel heroInformationPanel;
    DeckCreator deckCreator;

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
        deckCreator = FindObjectOfType<DeckCreator>(includeInactive: true);
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        infoButton.onClick.AddListener(OnInfoButtonClicked);
        useButton.onClick.AddListener(OnUseButtonClicked);
    }

    private void OnDisable()
    {
        if (infoButton != null) infoButton.onClick.RemoveListener(OnInfoButtonClicked);
        if (useButton != null) useButton.onClick.RemoveListener(OnUseButtonClicked);
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

    private void OnUseButtonClicked()
    {
        if (!deckCreator.IsDeckFull())
        {
            EnableSelectedPanel(false);
            deckCreator.AddCard(this);
        }
    }

    private void EnableSelectedPanel(bool enable)
    {
        selectedPanel.gameObject.SetActive(enable);
    }

    private void OnInfoButtonClicked()
    {
        heroInformationPanel.SetHero(hero);
        heroInformationPanel.gameObject.SetActive(true);
        EnableSelectedPanel(false);
    }

    [Serializable]
    public class CardFrameByRarity
    {
        public Sprite frame;
        public HeroRarity heroRarity;
    }
}
