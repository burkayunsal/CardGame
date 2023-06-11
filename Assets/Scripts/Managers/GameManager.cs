using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static bool canStart = false, isRunning = false;
    
    public static void OnStartGame()
    {
        if (isRunning || !canStart) return;

        isRunning = true;
        canStart = false;
        UIManager.I.OnGameStarted();
    }

    public static void OnWin()
    {
        isRunning = false;
        canStart = false;
        UIManager.I.OnSuccess();
    }

    public static void OnLose()
    {
        isRunning = false;
        canStart = false;
        UIManager.I.OnFail();
    }

    public static void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}