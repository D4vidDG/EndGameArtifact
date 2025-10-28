using System;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    //[SerializeField] BackgroundByRarity cardByRarity;
    [SerializeField] Image rarityBackground;
    [SerializeField] Image heroImage;


    public void SetHero(Hero hero)
    {
        heroImage.sprite = hero.GetPortrait();
    }
}
