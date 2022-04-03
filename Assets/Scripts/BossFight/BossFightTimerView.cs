using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Таймер босс-файта
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class BossFightTimerView : MonoBehaviour
{
    public int time;

    private BossFightController controller;
    private TMP_Text text;

    private void Start()
    {
        controller = FindObjectOfType<BossFightController>();
        text = GetComponent<TMP_Text>();

        text.text = string.Empty;

        BossFightController.onStartFight += OnStartFight;
        BossFightController.onEndFight += OnEndFight;
        BossFightController.onTimeChange += OnTimeChange;
    }

    private void OnDestroy()
    {
        BossFightController.onStartFight -= OnStartFight;
        BossFightController.onEndFight -= OnEndFight;
        BossFightController.onTimeChange -= OnTimeChange;
    }

    private void OnStartFight()
    {
        text.text = SecondsToString(controller.SecondsToFight);
    }

    private void OnEndFight(bool isSuccess)
    {
        //TODO: заменить эту рофлянку
        text.text = isSuccess ? "WIN" : "LOX";
    }

    private void OnTimeChange(int timeLeft)
    {
        text.text = SecondsToString(timeLeft);
    }

    private string SecondsToString(int seconds)
    {
        return (new TimeSpan(0, 0, seconds)).ToString(@"mm\:ss");
    }
}
