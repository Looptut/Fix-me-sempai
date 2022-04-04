using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolsAndMechanics.UserInterfaceManager;

/// <summary>
/// Контроллер состояния игры
/// </summary>
public class GameStateController : MonoBehaviour
{
    [SerializeField]
    private WindowData loseWindow;
    [SerializeField]
    private WindowData winWindow;
    /*
    [Space]
    [SerializeField]
    private List<Canvas> canvases;
    */

    private bool isWin;
    private bool isEnd;

    private void Start()
    {
        ProgressBar.onProgressEnd += OnProgressEnd;
        TimerDigital.onTimeIsOver += OnTimeIsOver;
    }

    private void OnDestroy()
    {
        ProgressBar.onProgressEnd -= OnProgressEnd;
        TimerDigital.onTimeIsOver -= OnTimeIsOver;
        Time.timeScale = 1;
    }

    private void OnProgressEnd(bool isWin)
    {
        //DisableCanvases();

        if (isEnd) return;

        isEnd = true;
        this.isWin = isWin;
        Invoke(nameof(SetWindow), 3);
    }

    private void OnTimeIsOver()
    {
        //DisableCanvases();

        if (isEnd) return;

        isEnd = true;
        Invoke(nameof(SetWindow), 3);
    }

    private void SetWindow()
    {
        WindowsController.Instance.SetWindow(isWin ? winWindow : loseWindow, false);
        Time.timeScale = 0;
    }

    /*
    private void DisableCanvases()
    {
        foreach(var c in canvases)
        {
            c.gameObject.SetActive(false);
        }
    }*/
}
