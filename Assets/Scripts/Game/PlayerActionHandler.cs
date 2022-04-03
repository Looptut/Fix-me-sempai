using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionHandler : MonoBehaviour
{
    [SerializeField]
    private float nearDistance = 1f;
    [SerializeField] private PlayerInput playerInput;

    private List<Worker> workers = new List<Worker>();

    private List<Worker> nearTiredWorkers = new List<Worker>();

    private Worker currentActiveWorker;

    private void Awake()
    {
        workers = new List<Worker>(FindObjectsOfType<Worker>());

        if (playerInput != null)
            playerInput.OnInputAction += MakeWorkerAction;
    }

    private void OnDestroy()
    {
        if (playerInput != null)
            playerInput.OnInputAction -= MakeWorkerAction;
    }

    private void MakeWorkerAction()
    {
        if (currentActiveWorker != null)
        {
            currentActiveWorker.PerformAction();
        }
    }

    private void Update()
    {
        nearTiredWorkers.Clear();

        foreach (var w in workers)
        {
            if (w.IsBeingTired && Vector3.Distance(w.CheckPoint.position, transform.position) <= nearDistance)
            {
                nearTiredWorkers.Add(w);
            }
        }

        currentActiveWorker = null;

        float minNear = 100;
        foreach (var w in nearTiredWorkers)
        {
            if (Vector3.Distance(w.CheckPoint.position, transform.position) < minNear)
            {
                currentActiveWorker = w;
            }
        }
    }
}
