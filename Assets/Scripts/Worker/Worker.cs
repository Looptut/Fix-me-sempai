using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public event Action<Worker> OnAction = delegate { };
    public event Action<Worker> OnBeingTired = delegate { };

    public Transform Bubble;
    public Vector3 BubblePosition => Bubble.position;

    [SerializeField] private bool isBeingTired = false;
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

    [ContextMenu("Switch tired")]
    public void SetTired()
    {
        IsBeingTired = !IsBeingTired;
    }

    public void PerformAction()
    {
        Debug.Log("I " + gameObject.name + " make Action");
        OnAction.Invoke(this);
        IsBeingTired = false;
    }
}