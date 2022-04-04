using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudio : MonoBehaviour
{
    public List<Worker> workers;

    public AudioSource source;

    private int counter;

    private void Start()
    {
        workers = new List<Worker>(FindObjectsOfType<Worker>());
        foreach (var worker in workers)
        {
            worker.OnBeingTired += HandleFire;
        }
        Worker.OnStateChange += HandleStop;
    }

    private void HandleStop(bool arg1, bool arg2)
    {
        counter--;
        if (counter == 0)
        {
            source.Stop();
        }
    }

    private void OnDestroy()
    {
        Worker.OnStateChange -= HandleStop;
        foreach (var worker in workers)
        {
            if (worker != null)
                worker.OnBeingTired -= HandleFire;
        }
    }

    private void HandleFire()
    {
        counter++;
        if (counter == 1)
        {
            source.Play();
        }
    }
}