using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
   private int _playerScore, _AIScore;
   public int playerCardNumber, AICardNumber;

   [SerializeField] private TextMeshProUGUI[] txtPlayerScore, txtAIScore;

   private int PlayerScore
   {
      get => _playerScore;

      set
      {
         _playerScore = value;
         foreach (TextMeshProUGUI txt in txtPlayerScore)
         {
            txt.text = "Player Score : " + _playerScore;
         }
      }
   }
   
   private int AIScore
   {
      get => _AIScore;

      set
      {
         _AIScore = value;
         foreach (TextMeshProUGUI txt in txtAIScore)
         {
            txt.text = "AI Score : " + _AIScore;
         }
      }
   }

   
   public void Pishti(bool isPlayer)
   {
      if (isPlayer)
      {
         PlayerScore += 10;
         AddCard(true,Configs.Game.PishtiPoint);
      }
      else
      {
         AIScore += 10;
         AddCard(false,Configs.Game.PishtiPoint);
      }
   }

   public void AddPoint(bool isPlayer, int count)
   {
      if (isPlayer)
      {
         PlayerScore += count;
      }
      else
      {
         AIScore += count;

      }
   }

   public void AddCard(bool isPlayer, int cardCount)
   {
      if (isPlayer)
      {
         playerCardNumber += cardCount;
      }
      else
      {
         AICardNumber += cardCount;
      }
   }

   public void OnLevelEnd()
   {
      if (playerCardNumber > AICardNumber)
      {
         PlayerScore += 3;
      }
      else if (AICardNumber > playerCardNumber)
      {
         AIScore += 3;
      }

      StartCoroutine(CheckWhoWins());
   }

   IEnumerator CheckWhoWins()
   {
      yield return new WaitForSeconds(2f);
      
      if (PlayerScore > AIScore)
      {
         GameManager.OnWin();
      }
      else
      {
         GameManager.OnLose();
      }
   }
}
