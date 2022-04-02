using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    private const string WORKER_TAG = "Worker";

    [SerializeField] private PlayerInput playerInput;

    private List<WorkerTriggerZone> workers = new List<WorkerTriggerZone>();
    private Worker currentActiveWorker;
    private void Awake()
    {
        var array = GameObject.FindGameObjectsWithTag(WORKER_TAG);

        WorkerTriggerZone zone;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].TryGetComponent(out zone))
            {
                workers.Add(zone);
            }
        }

        foreach (var worker in workers)
        {
            worker.OnEnterZone += SetActiveWorker;
        }

        if (playerInput != null)
            playerInput.OnInputAction += MakeWorkerAction;
    }
    private void SetActiveWorker(Worker worker)
    {
        currentActiveWorker = worker;
    }

    private void MakeWorkerAction()
    {
        if (currentActiveWorker != null)
            currentActiveWorker.PerformAction();
    }

    private void OnDestroy()
    {
        foreach (var worker in workers)
        {
            if (worker != null)
            {
                worker.OnEnterZone -= SetActiveWorker;
            }
        }
    }

}
