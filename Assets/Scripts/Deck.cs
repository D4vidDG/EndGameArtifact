using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    [SerializeField] RectTransform[] cardContainers;
    [SerializeField] Card deckCardPrefab;
    [SerializeField] Hero[] startHeroes;

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

        for (int i = 0; i < startHeroes.Length && i < cardContainers.Length; i++)
        {
            Hero hero = startHeroes[i];
            if (ContainsHero(hero)) continue;
            Card card = Instantiate(deckCardPrefab);
            card.SetHero(hero);
            AddCard(card);
        }
    }

    void OnEnable()
    {
        Card.OnRemoveButtonClicked += OnCardRemoveButtonClicked;
    }

    void OnDisable()
    {
        Card.OnRemoveButtonClicked -= OnCardRemoveButtonClicked;
    }

    public bool AddCard(Card card)
    {
        if (IsDeckFull()) return false;

        RectTransform container = emptyContainers.Dequeue();
        container.gameObject.SetActive(false);
        availableContainers.Enqueue(container);

        Card cardClone = Instantiate(deckCardPrefab);
        cardClone.SetHero(card.GetHero());
        cardClone.transform.SetParent(this.transform);
        cardClone.transform.SetSiblingIndex(cardsInDeck.Count);
        cardsInDeck.Add(cardClone);

        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);

        return true;
    }

    public bool ContainsHero(Hero hero)
    {
        if (IsDeckEmpty()) return false;

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
