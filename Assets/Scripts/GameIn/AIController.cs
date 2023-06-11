using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AIController : Singleton<AIController>
{
    public List<Card> lsAICards = new List<Card>();

    public void SetAI()
    {
        lsAICards.AddRange(CardManager.I.GetPlayerCards(1));
    }

    public void AIPlay()
    {
        StartCoroutine(Play());
    }
    
    private IEnumerator Play()
    {
        yield return new WaitForSeconds(0.75f);
        if (TurnManager.I.activeTurn == Turn.AI)
        {
            Card card = ChooseCard();
            
            if (lsAICards.Contains(card))
            {
                lsAICards.Remove(card);
                CardManager.I.OnPlayerUsesCard(1, card);

                if (lsAICards.Count == 0)
                {
                    yield return new WaitForSeconds(0.5f);
                    CardManager.I.CheckGameEnd();
                }
            }
        }
    }
    

    Card ChooseCard()
    {
        if (CardChecker.I.TopCard != null)
        {
            foreach (Card card in lsAICards)
            {
                if (card.value == CardChecker.I.TopCard.value)
                {
                    return card;
                }
            }
        }
        return lsAICards[Random.Range(0, lsAICards.Count)];
    }

    public void TakeCards(Queue<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.OnTaken(transform);
        }
    }
}
