using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] RectTransform[] cardContainers;
    [SerializeField] Card deckCardPrefab;

    List<Card> cardsInDeck;
    Queue<RectTransform> availableContainers;
    Queue<RectTransform> emptyContainers;
    int CardLimit => cardContainers.Length;

    RectTransform rectTransform;

    void Awake()
    {
        cardsInDeck = new List<Card>();
        rectTransform = GetComponent<RectTransform>();
        availableContainers = new Queue<RectTransform>();
        emptyContainers = new Queue<RectTransform>(cardContainers);
    }

    void OnEnable()
    {
        Card.OnRemoveButtonClicked += OnCardRemoveButtonClicked;
    }

    void OnDisable()
    {
        Card.OnRemoveButtonClicked -= OnCardRemoveButtonClicked;
    }

    public void AddCard(Card card)
    {
        if (IsDeckFull()) return;

        RectTransform container = emptyContainers.Dequeue();
        container.gameObject.SetActive(false);
        availableContainers.Enqueue(container);

        Card cardClone = Instantiate(deckCardPrefab);
        cardClone.SetHero(card.GetHero());
        cardClone.transform.SetParent(this.transform);
        cardClone.transform.SetSiblingIndex(cardsInDeck.Count);
        cardsInDeck.Add(cardClone);

        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    }

    public bool ContainsHero(Hero hero)
    {
        if (!IsDeckEmpty()) return true;

        foreach (Card card in cardsInDeck)
        {
            if (card.GetHero() == hero) return true;
        }

        return false;
    }

    public bool ContainsCard(Card card)
    {
        return !IsDeckEmpty() && cardsInDeck.Contains(card);
    }

    private void OnCardRemoveButtonClicked(Card card)
    {
        RemoveCard(card);
    }

    private void RemoveCard(Card card)
    {
        if (!ContainsCard(card)) return;

        RectTransform container = availableContainers.Dequeue();
        container.SetSiblingIndex(transform.childCount - 1);
        container.gameObject.SetActive(true);
        emptyContainers.Enqueue(container);

        cardsInDeck.Remove(card);

        Destroy(card.gameObject);
    }

    public bool IsDeckFull()
    {
        return cardsInDeck.Count >= CardLimit;
    }

    public bool IsDeckEmpty()
    {
        return cardsInDeck.Count == 0;
    }

    public List<Card> GetCards()
    {
        return cardsInDeck;
    }

}
