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
    [SerializeField] LevelUpMeter levelUpMeter;

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

    void OnDestroy()
    {
        if (hero != null) hero.OnLevelUp -= OnHeroLevelUp;
    }

    public Hero GetHero()
    {
        return hero;
    }

    public void SetHero(Hero hero)
    {
        if (hero != null) hero.OnLevelUp -= OnHeroLevelUp;

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

        levelUpMeter.SetHero(hero);
        levelUpMeter.UpdateUI();

        this.hero = hero;

        hero.OnLevelUp += OnHeroLevelUp;
    }


    public void OnSelect(BaseEventData eventData)
    {
        level.transform.parent.gameObject.SetActive(false);
        levelUpMeter.gameObject.SetActive(false);
        EnableSelectedPanel(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        level.transform.parent.gameObject.SetActive(true);
        levelUpMeter.gameObject.SetActive(true);
        EnableSelectedPanel(false);
    }


    private void OnHeroLevelUp()
    {
        level.text = hero.GetLevel().ToString();
    }


    private void EnableSelectedPanel(bool enable)
    {
        selectedPanel.gameObject.SetActive(enable);
    }

    private void OnInfoClicked()
    {
        heroInformationPanel.SetHero(hero);
        heroInformationPanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EnableSelectedPanel(false);
    }

    private void OnUseClicked()
    {
        EnableSelectedPanel(false);
        EventSystem.current.SetSelectedGameObject(null);
        OnUseButtonClicked.Invoke(this);
    }
    private void OnRemoveClicked()
    {
        EnableSelectedPanel(false);
        EventSystem.current.SetSelectedGameObject(null);
        OnRemoveButtonClicked.Invoke(this);
    }
}
