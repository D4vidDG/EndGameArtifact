using UnityEngine;

[CreateAssetMenu(fileName = "Heroe", menuName = "Create New Hero", order = 0)]
public class Heroe : ScriptableObject
{
    [SerializeField] HeroeClass heroeClass;
    [SerializeField] HeroeRarity rarity;
    [SerializeField] Sprite portrait;
}

public enum HeroeRarity
{
    Rare,
    Epic,
    Legendary
}

public enum HeroeClass
{
    Sniper,
    Tank,
    Assassin,
    Support,
    Damage,
    Demolisher,
    Utility
}