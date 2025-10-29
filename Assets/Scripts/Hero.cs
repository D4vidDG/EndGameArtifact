using UnityEngine;

[CreateAssetMenu(fileName = "Heroe", menuName = "Create New Hero", order = 0)]
public class Hero : ScriptableObject
{
    [SerializeField] HeroClass heroeClass;
    [SerializeField] HeroRarity rarity;
    [SerializeField] Sprite portrait;
    [SerializeField] int level = 1;

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
        return level;
    }

    public string GetName()
    {
        return this.name;
    }

    public HeroClass GetClass()
    {
        return heroeClass;
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