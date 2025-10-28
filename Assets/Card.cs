using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class Card : MonoBehaviour
{
    //[SerializeField] BackgroundByRarity cardByRarity;
    [SerializeField] Image rarityFrame;
    [SerializeField] Image heroImage;
    [SerializeField] CardFrameByRarity[] cardFrames;

    Dictionary<HeroRarity, Sprite> cardFrameByRarity;
    Hero hero;

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
    }

    public Hero GetHero()
    {
        return hero;
    }

    public void SetHero(Hero hero)
    {
        heroImage.sprite = hero.GetPortrait();
        Sprite frame = cardFrameByRarity[hero.GetRarity()];
        if (frame != null) rarityFrame.sprite = frame;
        this.hero = hero;
    }

    [Serializable]
    public class CardFrameByRarity
    {
        public Sprite frame;
        public HeroRarity heroRarity;
    }
}
