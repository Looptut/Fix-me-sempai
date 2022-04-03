using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Вью босс-файта
/// </summary>
public class BossFightView : MonoBehaviour
{
    [SerializeField]
    [Header("Задержка перед выключением окна после окончания файта")]
    private float delay;
    [SerializeField]
    private GameObject bossWindow;
    [SerializeField]
    private Image bossImage;

    private BossFightController controller;

    private void Start()
    {
        controller = FindObjectOfType<BossFightController>();

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
        if (controller.BossIcon != null)
            bossImage.sprite = controller.BossIcon;

        bossWindow.SetActive(true);
    }

    private void OnEndFight(bool isSuccess)
    {
        Invoke(nameof(DisableWindow), delay);
    }

    private void DisableWindow()
    {
        bossWindow.SetActive(false);
    }
}
