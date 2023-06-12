using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : Singleton<CardManager>
{
    [SerializeField] private Transform tableSlot;
    [SerializeField] private List<Transform> playerSlots;
    [SerializeField] private TextMeshProUGUI txtCardsLeft;
    
    private List<List<Card>> playersHoldings = new List<List<Card>>();
    public Queue<Card> _availableCards = new();
    public Queue<Card> tableCards = new();
    
    public List<Card> GetPlayerCards(int id) => playersHoldings[id];
    
    public void OnStart()
    {
        StartCoroutine(InitGame());
    }
    
    public IEnumerator InitGame()
    {
        SetUpPlayers();
        
        DeckManager.I.CreateDeck();
        
        foreach (Card card in DeckManager.I.deck)
            _availableCards.Enqueue(card);
        
        DraftForPlayers();
    
        DraftForTable();
    
        yield return null;
        
        GameManager.canStart = true;
    }
    
    private void SetUpPlayers()
    {
        int playerCount = Configs.Player.playerCount;
        
        for (int i = 0; i < playerCount; i++)
            playersHoldings.Add(new List<Card>());
    }
    
    public void DraftForPlayers()
    {
        for (int i = 0; i < Configs.Player.playerCount; i++)
        {
            for (int j = 0; j < 4; j++)
                playersHoldings[i].Add(_availableCards.Dequeue());
        }

        txtCardsLeft.text = _availableCards.Count.ToString("00");
        StartCoroutine(DrawCards());
        
        PlayerController.I.SetPlayer();
        AIController.I.SetAI();
    }

    IEnumerator DrawCards()
    {
        for (int i = 0; i < Configs.Player.playerCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Card card = playersHoldings[i][j];
                int temp = i;
                card.transform.DOMove(playerSlots[i].transform.position, 0.5f)
                    .OnComplete( () =>
                    {
                        card.transform.SetParent(playerSlots[temp]);
                    });
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
    
    private void DraftForTable()
    {
        // Set up initial cards on table.
        for (int j = 0; j < 4; j++)
        {
            Card card = _availableCards.Dequeue();
            tableCards.Enqueue(card);
            card.transform.SetParent(tableSlot);
            card.transform.localPosition = Vector3.zero;
            card.transform.localEulerAngles = Vector3.forward * GetCardAngle(j);
             
            if (j == 3)
            {
                card.ShowCardFace(true);
                CardChecker.I.TopCard = card;
            }
        }
    }
    
    public void OnPlayerUsesCard(int playerID, Card card)
    {
        playersHoldings[playerID].Remove(card);
        card.transform.SetParent(tableSlot);
        card.ShowCardFace(true);
        tableCards.Enqueue(card);
        card.transform.DOLocalRotate(Vector3.forward * GetCardAngle(Random.Range(0,4)), 0.5f);
        card.transform.DOLocalMove(Vector3.zero, 0.5f).OnComplete( () =>
        {
            CardChecker.I.Check(playerID,card);
            TurnManager.I.SwitchTurn();
        });
        
    }
    
    public void ClearTable()
    {
        tableCards.Clear();
    }
    
    private float GetCardAngle(int index) => (-10f * index + 15);

    public void CheckGameEnd()
    {
        if (_availableCards.Count == 0)
        {
            if (CardChecker.I.isLastTakerPlayer)
            {
                CardChecker.I.MoveCardsToLastTaker(true,tableCards);
            }
            else
            {
                CardChecker.I.MoveCardsToLastTaker(false,tableCards);
            }
            ScoreManager.I.OnLevelEnd();
        }
        else
        {
            DraftForPlayers();
        }
    }

}