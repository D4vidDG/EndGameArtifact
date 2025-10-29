using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DeckSelectionButton : MonoBehaviour
{
    [SerializeField] int deckNumber;
    [SerializeField] TextMeshProUGUI text;

    Button button;
    DeckSelector deckSelector;

    void Awake()
    {
        button = GetComponent<Button>();
        deckSelector = FindObjectOfType<DeckSelector>();
    }

    void Start()
    {
        text.text = deckNumber.ToString();
    }

    void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        if (deckSelector != null) deckSelector.SelectDeck(deckNumber);
    }
}
