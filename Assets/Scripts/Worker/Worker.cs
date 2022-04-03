using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Worker : MonoBehaviour
{
    public UnityEvent ActionDone = new UnityEvent();

    public event Action<Worker> OnAction = delegate { };
    public event Action<Worker> OnBeingTired = delegate { };
    public static Action OnBeingBoss = delegate { };

    [SerializeField] private BossFightController boss;
    [SerializeField] private PlayerInput player;
    [SerializeField] private Transform fire;
    [SerializeField] private bool isBeingTired = false;
    [SerializeField] private bool isBeingBoss = false;
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
                    OnBeingTired.Invoke(this);
                    if (fire != null)
                        fire.gameObject.SetActive(true);
                }
            }
        }
    }
    public bool IsBeingBoss
    {
        get => isBeingBoss;
        set => isBeingBoss = value;
    }

    private void Start()
    {
        if (fire != null)
            fire.gameObject.SetActive(false);
    }


    [ContextMenu("Switch tired")]
    public void SwitchTired()
    {
        IsBeingTired = !IsBeingTired;
    }
    [ContextMenu("Switch boss")]
    public void SwitchBoss()
    {
        IsBeingBoss = !IsBeingBoss;
    }

    public virtual void PerformAction()
    {
        OnAction.Invoke(this);

        if (IsBeingTired)
        {
            fire.gameObject.SetActive(false);
            player.DoAction(fire);
        }
        if (isBeingBoss)
        {
            boss.StartBossFight();
        }
        ActionDone.Invoke();
    }
}