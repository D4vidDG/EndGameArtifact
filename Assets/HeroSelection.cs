using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class HeroSelection : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Hero[] heroes;
    [SerializeField] TMP_Dropdown orderByDropdown;


    Dictionary<OptionData, CardDisplayParams> displayParamsByDropdownOption;
    Card[] cards;

    void Awake()
    {
        displayParamsByDropdownOption = new Dictionary<OptionData, CardDisplayParams>();
    }

    void Start()
    {
        PopulateGrid();
        SetDropdownOptions();
        SortCards();
    }

    private void PopulateGrid()
    {
        cards = new Card[heroes.Length];
        for (int i = 0; i < cards.Length; i++)
        {
            Card cardInstance = Instantiate(cardPrefab);
            cardInstance.SetHero(heroes[i]);
            cards[i] = cardInstance;
        }

    }

    public void SortCards()
    {
        grid.transform.DetachChildren();

        CardDisplayParams displayParams = GetDisplayParams();

        switch (displayParams.filter)
        {
            case CardDisplayFilter.Rarity:
                Array.Sort(cards, new CardRarityComparer());
                break;
            case CardDisplayFilter.Level:
                Array.Sort(cards, new CardLevelComparer());
                break;
        }

        switch (displayParams.order)
        {
            case CardDisplayOrder.Ascending:

                foreach (Card card in cards)
                {
                    card.transform.parent = grid;
                }
                break;

            case CardDisplayOrder.Descending:

                for (int i = cards.Length - 1; i >= 0; i--)
                {
                    cards[i].transform.parent = grid;
                }

                break;
        }
    }

    private CardDisplayParams GetDisplayParams()
    {
        OptionData selectedOption = orderByDropdown.options[orderByDropdown.value];

        if (displayParamsByDropdownOption.ContainsKey(selectedOption))
        {
            return displayParamsByDropdownOption[selectedOption];
        }
        else
        {
            Debug.LogError("OrderBy Dropdown opton is not defined. Option:" + selectedOption.text);
            return default;
        }
    }

    private void SetDropdownOptions()
    {
        foreach (CardDisplayFilter filter in Enum.GetValues(typeof(CardDisplayFilter)))
        {
            foreach (CardDisplayOrder order in Enum.GetValues(typeof(CardDisplayOrder)))
            {
                string dataText = filter.ToString() + " " + order.ToString();
                OptionData optionData = new(dataText);
                orderByDropdown.options.Add(optionData);
                CardDisplayParams displayParams = new(filter, order);
                displayParamsByDropdownOption.Add(optionData, displayParams);
            }
        }
    }

    public class CardRarityComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            int diff = (int)x.GetHero().GetRarity() - (int)y.GetHero().GetRarity();
            //sort alphabetically by default
            if (diff == 0)
            {
                return String.Compare(x.GetHero().GetName(), y.GetHero().GetName(), StringComparison.CurrentCultureIgnoreCase);
            }

            return diff;
        }
    }

    public class CardLevelComparer : IComparer<Card>
    {
        public int Compare(Card x, Card y)
        {
            int diff = (int)x.GetHero().GetLevel() - (int)y.GetHero().GetLevel();
            //sort alphabetically by default
            if (diff == 0)
            {
                return String.Compare(x.GetHero().GetName(), y.GetHero().GetName(), StringComparison.CurrentCultureIgnoreCase);
            }

            return diff;
        }
    }

    public struct CardDisplayParams
    {
        public CardDisplayOrder order;
        public CardDisplayFilter filter;

        public CardDisplayParams(CardDisplayFilter filter, CardDisplayOrder order)
        {
            this.order = order;
            this.filter = filter;
        }
    }
}

public enum CardDisplayOrder
{
    Descending,
    Ascending
}

public enum CardDisplayFilter
{
    Rarity,
    Level,
}
