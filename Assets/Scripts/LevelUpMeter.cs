using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMeter : MonoBehaviour
{
    [SerializeField] UIBar progressionBar;
    [SerializeField] TextMeshProUGUI progressionText;
    [SerializeField] Image arrow;
    [SerializeField] Color defaultColor;
    [SerializeField] Color maxLevelColor;

    Hero hero;


    void OnDestroy()
    {
        if (hero != null)
        {
            hero.OnLevelUp -= UpdateUI;
            hero.OnNewCarsCollected -= UpdateUI;
        }
    }

    public void SetHero(Hero hero)
    {
        this.hero = hero;
        hero.OnLevelUp += UpdateUI;
        hero.OnNewCarsCollected += UpdateUI;
    }

    public void UpdateUI()
    {
        if (hero != null)
        {
            if (hero.IsAtMaxLevel())
            {
                arrow.gameObject.SetActive(false);
                progressionText.text = "MAX LEVEL";
                progressionBar.SetPercentage(100f);
                progressionBar.SetColor(maxLevelColor);
            }
            else
            {
                arrow.gameObject.SetActive(true);
                progressionText.text = hero.GetCardsCollectedToLevelUp() + "/" + hero.GetCardsToLevelUp();
                progressionBar.SetPercentage(hero.GetCardsCollectedToLevelUp() * 100f / hero.GetCardsToLevelUp());
                progressionBar.SetColor(defaultColor);
            }
        }
    }
}
