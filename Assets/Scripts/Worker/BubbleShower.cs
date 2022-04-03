using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShower : MonoBehaviour
{
    [SerializeField] private WorkerTriggerZone zone;
    [SerializeField] private Worker worker;
    [SerializeField] private SpriteRenderer sprite;
    private void Awake()
    {
        if (zone != null)
        {
            zone.OnEnterZone += ShowBubble;
            zone.OnExitZone += HideBubble;
        }
        if (worker != null)
        {
            worker.OnAction += SetActivity;
        }
        HideBubble(default);
    }

    private void SetActivity(Worker worker)
    {
        HideBubble(worker);
    }

    private void HideBubble(Worker worker)
    {
        SetActivity(false);
    }

    private void ShowBubble(Worker worker)
    {
        SetActivity((worker.IsBeingTired || worker.IsBeingBoss) && true);
    }

    private void SetActivity(bool isActive)
    {
        sprite.gameObject.SetActive(isActive);
    }
    private void OnDestroy()
    {
        if (zone != null)
        {
            zone.OnEnterZone -= ShowBubble;
            zone.OnExitZone -= HideBubble;
        }
        if (worker != null)
        {
            worker.OnAction -= SetActivity;
        }
    }
}
