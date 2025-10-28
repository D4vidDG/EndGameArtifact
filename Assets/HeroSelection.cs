using UnityEditor;
using UnityEngine;

public class HeroSelection : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Hero[] heroes;

    void Start()
    {
        PopulateGrid();
    }

    private void PopulateGrid()
    {
        foreach (Hero hero in heroes)
        {
            Card cardInstance = Instantiate(cardPrefab);
            cardInstance.SetHero(hero);
            cardInstance.transform.parent = grid;
        }
    }
}
