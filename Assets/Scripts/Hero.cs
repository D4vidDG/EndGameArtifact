using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Heroe", menuName = "Create New Hero", order = 0)]
public class Hero : ScriptableObject
{

    const int MAX_LEVEL = 10;
    [SerializeField] HeroClass heroeClass;
    [SerializeField] HeroRarity rarity;
    [SerializeField] Sprite portrait;
    [SerializeField] HeroProgression heroProgression;

    int cardsCollected = 0;
    int currentLevel;

    public Action OnNewCarsCollected;
    public Action OnLevelUp;

    public static Action OnAnyHeroLevelUp;

    void OnEnable()
    {
        currentLevel = Random.Range(1, MAX_LEVEL + 1);
        if (heroProgression != null) cardsCollected = Random.Range(0, GetCardsToLevelUp() + 1);
    }

    public Sprite GetPortrait()
    {
        return portrait;
    }

    public HeroRarity GetRarity()
    {
        return rarity;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public string GetName()
    {
        return this.name;
    }

    public HeroClass GetClass()
    {
        return heroeClass;
    }

    public float GetStatValue(HeroStat stat)
    {
        return heroProgression.GetStat(stat, currentLevel);
    }

    public float GetStatValueAtLevel(HeroStat stat, int level)
    {
        return heroProgression.GetStat(stat, level);
    }

    public int GetCardsToLevelUp()
    {
        return (int)GetStatValue(HeroStat.CardsToUpgrade);
    }

    public int GetCardsCollectedToLevelUp()
    {
        return cardsCollected;
    }

    public void AddCardsToLevelUp(int amount)
    {
        if (cardsCollected <= GetCardsToLevelUp())
        {
            cardsCollected = Mathf.Min(cardsCollected + amount, GetCardsToLevelUp());
            OnNewCarsCollected?.Invoke();
        }
    }

    public bool IsAtMaxLevel()
    {
        return currentLevel >= MAX_LEVEL;
    }


    public bool CanLevelUp()
    {
        return cardsCollected >= GetCardsToLevelUp() && !IsAtMaxLevel();
    }

    public void LevelUp()
    {
        if (currentLevel <= MAX_LEVEL)
        {
            currentLevel = Mathf.Min(currentLevel + 1, MAX_LEVEL);
            cardsCollected = 0;
            OnLevelUp?.Invoke();
            OnAnyHeroLevelUp?.Invoke();
        }
    }
}

public enum HeroRarity
{
    Rare,
    Epic,
    Legendary
}

public enum HeroClass
{
    Sniper,
    Tank,
    Assassin,
    Support,
    Damage,
    Demolisher,
    Utility
}