using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardChecker : Singleton<CardChecker>
{
   private Card _topCard;
   
   public Card TopCard
   {
      get => _topCard;

      set
      {
         _topCard = value;
         if (_topCard != null)
         {
            Debug.Log(TopCard.value);
         }
      }
   }

   public void Check(int playerID, Card card)
   {
      if (TopCard == null)
      {
         TopCard = card;
         return;
      }

      Queue<Card> cardsOnTable = CardManager.I.tableCards;
      
      if (card.value == CardValue.Jack) // If player uses a J.
      {
         if (TopCard.value == card.value)
         {
            ScoreManager.I.Pishti(playerID == 0);
         }
         else
         {
            ScoreManager.I.AddCard(playerID == 0,cardsOnTable.Count);
         }
         
         MoveCardsTo(playerID,cardsOnTable);
         
      } 
      else if (TopCard.value == card.value)
      {
         if (cardsOnTable.Count == 2)
         {
            ScoreManager.I.Pishti(playerID == 0);
         }
         else
         {
            ScoreManager.I.AddCard(playerID == 0,cardsOnTable.Count);
         }

         MoveCardsTo(playerID,cardsOnTable);
      }
      else
      {
         TopCard = card;
      }
   }

   void MoveCardsTo(int id, Queue<Card> cardsOnTable)
   {
      if (id == 0)
      {
         PlayerController.I.TakeCards(cardsOnTable);
      }
      else
      {
         AIController.I.TakeCards(cardsOnTable);
      }
      
      int tablePoint = CountTablePoint(cardsOnTable);
      ScoreManager.I.AddPoint(id == 0, tablePoint);
      CardManager.I.ClearTable();
      TopCard = null;
   }
   
   int CountTablePoint(Queue<Card> cards)
   {
      int count = 0;

      foreach (Card card in cards)
      {
            
         if (card.value == CardValue.Ace || card.value == CardValue.Jack)
         {
            count++;
         }

         if (card.type == CardType.Clubs && card.value == CardValue.Two)
         {
            count += 2;
         }
            
         if (card.type == CardType.Diamonds && card.value == CardValue.Ten)
         {
            count += 3;
         }
      }

      return count;
   }
   
}


