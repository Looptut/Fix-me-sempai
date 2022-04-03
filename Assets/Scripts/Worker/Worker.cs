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
                }
            }
        }
    }
    public bool IsBeingBoss() { return isBeingBoss; }
    [ContextMenu("Switch tired")]
    public void SwitchTired()
    {
        IsBeingTired = !IsBeingTired;
    }

    public virtual void PerformAction()
    {
        OnAction.Invoke(this);
        ActionDone.Invoke();
    }
}