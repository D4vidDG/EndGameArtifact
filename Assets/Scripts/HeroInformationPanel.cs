using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroInformationPanel : MonoBehaviour
{
    [SerializeField] Image heroImage;
    [SerializeField] TextMeshProUGUI heroName;
    [SerializeField] TextMeshProUGUI heroClass;

    public void SetHero(Hero hero)
    {
        if (hero != null)
        {
            heroName.text = hero.GetName();
            heroClass.text = hero.GetClass().ToString();
            heroImage.sprite = hero.GetPortrait();
        }
    }
}


