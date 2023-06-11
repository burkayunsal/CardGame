using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckManager : Singleton<DeckManager>
{
    public List<Card> deck = new List<Card>();

    [SerializeField] private Transform availableCards;
    public void CreateDeck()
    {
        for (int i = 0; i < Configs.Card.cardTypeNumber; i++)
        {
            CardType type = (CardType)i;
            
            for (int j = 0; j < Configs.Card.cardNumberOfEachType; j++)
            {
                CardValue value = (CardValue)j;

                Card card = PoolHandler.I.GetObject<Card>();
                card.transform.SetParent(availableCards);
                card.transform.localPosition = Vector3.zero;
                card.Init(type, value);
                deck.Add(card);
            }
        }
        ShuffleDeck();
    }
    
    
    private void ShuffleDeck()
    {
        int count = deck.Count;
        while (count > 1)
        {
            count--;
            int k = Random.Range(0, count + 1);
            Card temp = deck[k];
            deck[k] = deck[count];
            deck[count] = temp;
        }
    }
}
