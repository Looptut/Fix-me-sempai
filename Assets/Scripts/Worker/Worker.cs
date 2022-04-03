using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Worker : MonoBehaviour
{
    public static event Action ATTENTION_BOSS_ON_FIRE = delegate { };

    public UnityEvent ActionDone = new UnityEvent();

    public event Action<Worker> OnAction = delegate { };
    public event Action<Worker> OnBeingTired = delegate { };
    public static Action OnBeingBoss = delegate { };

    public Transform BubblePosition;
    public Transform CheckPoint => checkPoint;

    [SerializeField] private BossFightController bossFightController;
    [SerializeField] private PlayerInput player;
    [SerializeField] private Transform fire;
    [SerializeField] private bool isBeingTired = false;
    [SerializeField] private bool isBeingBoss = false;
    [SerializeField] private Transform checkPoint;

    public bool IsBeingTired
    {
        get => isBeingTired;
        set
        {
            if (isBeingTired != value)
            {
                isBeingTired = value;
                if (isBeingTired)
                {
                    if (IsBeingBoss)
                        ATTENTION_BOSS_ON_FIRE();
                    OnBeingTired.Invoke(this);
                    if (fire != null)
                        fire.gameObject.SetActive(true);
                }
            }
        }
    }
    public bool IsBeingBoss => isBeingBoss;

    private void Start()
    {
        bossFightController = FindObjectOfType<BossFightController>();

        if (fire != null)
            fire.gameObject.SetActive(false);
    }


    [ContextMenu("Switch tired")]
    public void SwitchTired()
    {
        IsBeingTired = !IsBeingTired;
    }

    public virtual void PerformAction()
    {
        OnAction.Invoke(this);

        if (IsBeingTired && !IsBeingBoss)
        {
            fire.gameObject.SetActive(false);
            IsBeingTired = false;
            player.DoAction(fire);
        }
        if (IsBeingTired && IsBeingBoss)
        {
            BossFightController.onEndFight += OnEndFight;
            bossFightController.StartBossFight();
        }
        ActionDone.Invoke();
    }

    private void OnEndFight(bool isSuccess)
    {
        if (isSuccess)
        {
            fire.gameObject.SetActive(false);
            IsBeingTired = false;
            player.DoAction(fire);
        }
        BossFightController.onEndFight -= OnEndFight;
    }
}