using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PolygonCollider2D))]
public class WorkerTriggerZone : MonoBehaviour
{
    public event Action<Worker> OnEnterZone = delegate { };
    public event Action<Worker> OnExitZone = delegate { };

    [SerializeField] private Worker worker;
    [SerializeField] private CustomEvent eventAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnterZone.Invoke(worker);
        eventAction.TryChangeActivityTo(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExitZone.Invoke(worker);
        eventAction.TryChangeActivityTo(false);
    }
}
