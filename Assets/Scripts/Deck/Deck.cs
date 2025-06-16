using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    [SerializeField] List<CardData> cards;

    List<CardData> cardPile;
    public List<CardData> CardPile => cardPile;

    private void Awake()
    {
        cardPile = new List<CardData>(cards);
        Shuffle();
    }

    public void Shuffle()
    {
        if(cardPile.Count == 0)
        {
            Debug.LogWarning("Cards pool is empty");
        }
        List<CardData> shufflingCards = new List<CardData>();
        List<CardData> _cardPile = new List<CardData>();
        int index = 0;

        while (shufflingCards.Count > 0)
        {
            index = Random.Range(0, shufflingCards.Count);
            _cardPile.Add(shufflingCards[index]);
            shufflingCards.RemoveAt(index);
        }

        cardPile.Clear();
        cardPile = new List<CardData>(_cardPile);
    }
}
