using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public List<Card> lsPlayerCards = new List<Card>();

    public void SetPlayer()
    {
        lsPlayerCards.AddRange(CardManager.I.GetPlayerCards(0));
        
        foreach (Card card in lsPlayerCards)
        {
            card.SetButtonInteractable(true);
            card.ShowCardFace(true);
        }
    }

    public void UseCard(Card card)
    {
        if (lsPlayerCards.Contains(card))
        {
            lsPlayerCards.Remove(card);
            CardManager.I.OnPlayerUsesCard(0, card);
        }
    }

    public void TakeCards(Queue<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.OnTaken(transform);
        }
    }
}