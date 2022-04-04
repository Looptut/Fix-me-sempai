using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupController : MonoBehaviour
{
    private CanvasGroup canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();

        BossFightController.onStartFight += OnStartFight;
        BossFightController.onEndFight += OnEndFight;
    }

    private void OnDestroy()
    {
        BossFightController.onStartFight -= OnStartFight;
        BossFightController.onEndFight -= OnEndFight;
    }

    private void OnStartFight()
    {
        canvas.alpha = 0;
    }

    private void OnEndFight(bool obj)
    {
        Invoke(nameof(Enable), 3);
    }

    private void Enable()
    {
        canvas.alpha = 1;
    }
}
