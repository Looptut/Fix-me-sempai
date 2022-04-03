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

    [Space]
    [SerializeField]
    private ParticleSystem smoke;
    [SerializeField]
    private ParticleSystem fire;

    private BossFightController controller;

    private void Start()
    {
        controller = FindObjectOfType<BossFightController>();

        bossWindow.SetActive(false);

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
        smoke.Stop();
        fire.Play();
    }

    private void OnEndFight(bool isSuccess)
    {
        if (isSuccess)
        {
            smoke.Play();
            fire.Stop();
        }

        Invoke(nameof(DisableWindow), delay);
    }

    private void DisableWindow()
    {
        bossWindow.SetActive(false);
    }
}
