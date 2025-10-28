using UnityEditor;
using UnityEngine;

public class HeroSelection : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Heroe[] heroes;


    private void PopulateGrid()
    {
        foreach (Heroe hero in heroes)
        {
            Card cardInstance = Instantiate(cardPrefab);
            cardInstance.SetHero(hero);
            cardInstance.transform.parent = grid;
        }
    }
}
