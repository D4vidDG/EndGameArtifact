using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckCreator : MonoBehaviour
{
    [SerializeField] Image[] cardContainers;
    [SerializeField] RectTransform deck;

    List<Card> cardsInDeck;
    int CardLimit => cardContainers.Length;

    void Awake()
    {
        cardsInDeck = new List<Card>();
    }

    public void AddCard(Card card)
    {
        if (IsDeckFull()) return;

        Image container = cardContainers[cardsInDeck.Count];
        container.gameObject.SetActive(false);
        card.transform.SetParent(container.transform.parent, false);
        card.transform.SetSiblingIndex(cardsInDeck.Count);
        LayoutRebuilder.MarkLayoutForRebuild(deck);
        cardsInDeck.Add(card);
    }

    public bool IsDeckFull()
    {
        return cardsInDeck.Count >= CardLimit;
    }
}
