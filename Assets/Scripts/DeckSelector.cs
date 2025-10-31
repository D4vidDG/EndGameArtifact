using System;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelector : MonoBehaviour
{
    [SerializeField] Deck[] decks;
    [SerializeField] int deckByDefault;
    [SerializeField] RectTransform deckSelectionButtons;

    public Deck CurrentDeck => currentDeck;
    Deck currentDeck;

    public Action<Deck, Deck> OnDeckChanged;


    void Start()
    {
        foreach (Deck deck in decks)
        {
            deck.gameObject.SetActive(false);
        }
        SelectDeck(deckByDefault);
        LayoutRebuilder.ForceRebuildLayoutImmediate(deckSelectionButtons);
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

        //LayoutRebuilder.MarkLayoutForRebuild(deckSelectionButtons);
    }
}
