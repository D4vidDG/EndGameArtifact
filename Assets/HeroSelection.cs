using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.TMP_Dropdown;

public class HeroSelection : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] RectTransform grid;
    [SerializeField] Hero[] heroes;
    [SerializeField] TMP_Dropdown sortByDropdown;
    [SerializeField] CardSortParams defaultSortParams;

    Dictionary<OptionData, CardSortParams> sortParamsByDropdownOption;
    Card[] cards;

    void Awake()
    {
        sortParamsByDropdownOption = new Dictionary<OptionData, CardSortParams>();
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

        CardSortParams sortParams = GetSortParams();

        Array.Sort(cards, new CardComparer(sortParams));

        foreach (Card card in cards)
        {
            card.transform.parent = grid;
        }
    }

    private CardSortParams GetSortParams()
    {
        OptionData selectedOption = sortByDropdown.options[sortByDropdown.value];

        if (sortParamsByDropdownOption.ContainsKey(selectedOption))
        {
            return sortParamsByDropdownOption[selectedOption];
        }
        else
        {
            Debug.LogError("OrderBy Dropdown opton is not defined. Option:" + selectedOption.text);
            return default;
        }
    }

    private void SetDropdownOptions()
    {
        OptionData defaultOption = null;

        foreach (CardSortFilter filter in Enum.GetValues(typeof(CardSortFilter)))
        {
            foreach (CardSortOrder order in Enum.GetValues(typeof(CardSortOrder)))
            {
                string dataText = filter.ToString() + " " + order.ToString();
                OptionData optionData = new(dataText);
                sortByDropdown.options.Add(optionData);
                CardSortParams displayParams = new(filter, order);
                sortParamsByDropdownOption.Add(optionData, displayParams);

                if (filter == defaultSortParams.filter && order == defaultSortParams.order)
                {
                    defaultOption = optionData;
                }
            }
        }

        if (defaultOption != null)
        {
            sortByDropdown.value = sortByDropdown.options.IndexOf(defaultOption);
        }
    }

    [Serializable]
    public struct CardSortParams
    {
        public CardSortOrder order;
        public CardSortFilter filter;

        public CardSortParams(CardSortFilter filter, CardSortOrder order)
        {
            this.order = order;
            this.filter = filter;
        }
    }

    public class CardComparer : IComparer<Card>
    {
        private CardSortParams sortParams;

        public CardComparer(CardSortParams sortParams)
        {
            this.sortParams = sortParams;
        }

        public int Compare(Card x, Card y)
        {
            int xValue = 0;
            int yValue = 0;

            switch (sortParams.filter)
            {
                case CardSortFilter.Rarity:
                    xValue = (int)x.GetHero().GetRarity();
                    yValue = (int)y.GetHero().GetRarity();
                    break;
                case CardSortFilter.Level:
                    xValue = x.GetHero().GetLevel();
                    yValue = y.GetHero().GetLevel();
                    break;
            }

            int order = 1;
            switch (sortParams.order)
            {
                case CardSortOrder.Ascending:
                    order = 1;
                    break;

                case CardSortOrder.Descending:

                    order = -1;
                    break;
            }

            int diff = (xValue - yValue) * order;

            //sort alphabetically by default
            if (diff == 0)
            {
                return String.Compare(x.GetHero().GetName(), y.GetHero().GetName(), StringComparison.CurrentCultureIgnoreCase);
            }

            return diff;
        }
    }

}


public enum CardSortOrder
{
    Ascending,
    Descending
}

public enum CardSortFilter
{
    Rarity,
    Level,
}
