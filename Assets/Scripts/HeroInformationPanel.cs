using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroInformationPanel : MonoBehaviour
{
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroName;
    [SerializeField] TextMeshProUGUI heroClass;
    [SerializeField] TextMeshProUGUI heroRarity;
    [SerializeField] Image classSymbol;
    [SerializeField] UIGradient basicInfoFrameGradient;
    [SerializeField] LevelBar levelBar;
    [SerializeField] UISettings uISettings;
    [SerializeField] LevelUpMeter levelUpMeter;
    [SerializeField] Button upgradeButton;

    StatUI[] stats
    {
        get
        {
            if (_stats == null) _stats = GetComponentsInChildren<StatUI>(includeInactive: true);
            return _stats;
        }
    }
    StatUI[] _stats;

    Hero hero;

    void Update()
    {
        if (hero != null && hero.CanLevelUp())
        {
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.interactable = false;
        }
    }

    public void SetHero(Hero hero)
    {
        if (hero != null)
        {
            this.hero = hero;
            UpdateUI();
        }
    }

    public void HideStatsIncrease()
    {
        if (!upgradeButton.interactable) return;
        foreach (StatUI stat in stats)
        {
            stat.ShowIncrease(false);
        }
    }

    public void ShowStatsIncrease()
    {
        if (hero == null) return;
        if (!upgradeButton.interactable) return;
        if (hero.IsAtMaxLevel()) return;
        foreach (StatUI stat in stats)
        {
            stat.ShowIncrease(true);
        }
    }

    private void UpdateUI()
    {
        heroImage.sprite = hero.GetPortrait();
        heroName.text = hero.GetName();
        heroClass.text = hero.GetClass().ToString();
        heroRarity.text = hero.GetRarity().ToString();
        heroRarity.color = uISettings.GetUIInfo(hero.GetRarity()).gradientColorBottom;
        classSymbol.sprite = uISettings.GetUIInfo(hero.GetClass()).symbol;
        basicInfoFrameGradient.m_color1 = uISettings.GetUIInfo(hero.GetRarity()).gradientColorTop;
        basicInfoFrameGradient.m_color2 = uISettings.GetUIInfo(hero.GetRarity()).gradientColorBottom;

        levelBar.SetLevel(hero.GetLevel());

        levelUpMeter.SetHero(hero);
        levelUpMeter.UpdateUI();

        UpdateStats();
    }

    private void UpdateStats()
    {
        foreach (StatUI statUI in stats)
        {
            float statCurrentValue = hero.GetStatValue(statUI.targetStat);

            statUI.SetValue(statCurrentValue);

            if (!hero.IsAtMaxLevel())
            {
                float statNextValue = hero.GetStatValueAtLevel(statUI.targetStat, hero.GetLevel() + 1);
                float increase = statNextValue - statCurrentValue;
                statUI.SetIncrease(increase);
            }
        }
    }

    public void IncreaseCardsOfCurrentHero()
    {
        if (hero != null)
        {
            hero.AddCardsToLevelUp((int)Mathf.Ceil(hero.GetCardsToLevelUp() * 0.1f));
        }
    }

    public void IncreaseLevelOfCurrentHero()
    {
        if (hero != null)
        {
            hero.LevelUp();
            UpdateUI();
        }
    }
}


