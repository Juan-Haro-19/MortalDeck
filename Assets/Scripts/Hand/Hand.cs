using System;
using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public List<CardData> cards;
    public CardPresenter presenter;
    private int index;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] AdjustInContainter hand;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (index >= cards.Count - 1)
                index = 0;
            else
            {
                index++;
            }

            if (hand.ItemsInContainer < 10)
            {
                hand.AddItem(CreateCard().GetComponent<RectTransform>());
            }
        }
    }

    GameObject CreateCard()
    {
        GameObject cardInstance = Instantiate(cardPrefab, hand.Container);
        presenter.SetCard(
            cards[index],
            cardPrefab.GetComponent<CardView>()
        );

        return cardInstance;
    }
}
