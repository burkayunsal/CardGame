using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Turn
{
    Player = 0,
    AI = 1
}
public class TurnManager : Singleton<TurnManager>
{
    public Turn activeTurn;
    [SerializeField] private Image screenSaver;

    private void Start()
    {
        activeTurn = Turn.Player;
        screenSaver.raycastTarget = false;
    }

    public void SwitchTurn()
    {
        activeTurn = (activeTurn == Turn.Player) ? Turn.AI : Turn.Player;

        if (activeTurn == Turn.Player)
        {
            screenSaver.raycastTarget = false;
        }
        else
        {
            screenSaver.raycastTarget = true;
            AIController.I.AIPlay();
        }
    }
}
