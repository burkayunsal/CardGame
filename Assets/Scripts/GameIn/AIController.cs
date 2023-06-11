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
        if (TurnManager.I.activeTurn == Turn.AI)
        {
            yield return new WaitForSeconds(0.75f);
            
            Card card = ChooseCard();
            lsAICards.Remove(card);
            CardManager.I.OnPlayerUsesCard(1, card);

            if (lsAICards.Count == 0)
            {
                yield return new WaitForSeconds(0.5f);
                CardManager.I.CheckGameEnd();
            }
            
        }
    }
    

    Card ChooseCard()
    {
        Card topCard = CardChecker.I.TopCard;
        if (topCard != null)
        {
            foreach (Card card in lsAICards)
            {
                if (card.value == topCard.value)
                {
                    return card;
                }
            }
        }
        else
        {
            return ReturnRandomCardExceptJack();
            
        }
        return lsAICards[Random.Range(0, lsAICards.Count)];
    }

    Card ReturnRandomCardExceptJack()
    {
        Card card;
        do
        {
            card = lsAICards[Random.Range(0, lsAICards.Count)];
        }
        while (card.value == CardValue.Jack);

        return card;
    }
    
    public void TakeCards(Queue<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.OnTaken(transform);
        }
    }
}
