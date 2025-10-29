using System;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelector : MonoBehaviour
{
    [SerializeField] Deck[] decks;
    [SerializeField] int deckByDefault;

    public Deck CurrentDeck => currentDeck;
    Deck currentDeck;
    RectTransform rectTransform;

    public Action<Deck, Deck> OnDeckChanged;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
        foreach (Deck deck in decks)
        {
            deck.gameObject.SetActive(false);
        }
        SelectDeck(deckByDefault);
    }

    public void SelectDeck(int number)
    {
        if (number < 1 || decks.Length < number) return;

        Deck selectedDeck = decks[number - 1];

        if (currentDeck == selectedDeck) return;

        Deck previousDeck = currentDeck;
        if (previousDeck != null)
        {
            previousDeck.gameObject.SetActive(false);
        }

        selectedDeck.gameObject.SetActive(true);
        currentDeck = selectedDeck;

        OnDeckChanged?.Invoke(previousDeck, currentDeck);

        LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
    }
}
