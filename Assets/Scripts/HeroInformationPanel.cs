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

    public void SetHero(Hero hero)
    {
        if (hero != null)
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
        }
    }
}


