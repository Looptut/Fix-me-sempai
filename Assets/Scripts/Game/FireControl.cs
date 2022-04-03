using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProgressBar))]
public class FireControl : MonoBehaviour
{
    private const string WORKER_TAG = "Worker";
    private List<Worker> workers = new List<Worker>();
    private List<Worker> workersSelected = new List<Worker>();
    private Worker worker;

    public FireCount fireCount;
    public ProgressBar progressBar;

    public class FireCount 
    {
        public FireCount() { }

        public int level;
        public int workers;
        public int bosses;
        public int time;
    }

    private void Awake()
    {
        fireCount = new FireCount();
        var array = GameObject.FindGameObjectsWithTag(WORKER_TAG);
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].TryGetComponent(out worker))
            {
                workers.Add(worker);
            }
        }
    }

    private void Start()
    {
        InvokeRepeating("setFire", 0, 1.0f);
    }
    void Update()
    {

    }

    private void setFire() 
    {
        int points = progressBar.GetCurrentPoints();
        if (points < 30) fireCount.level = 1;
        if (points > 29 && points < 70) fireCount.level = 2;
        if (points > 69) fireCount.level = 3;

        UpdateFireCount();

        workersSelected = new List<Worker>();
        foreach (Worker ow in workers)
            if (!ow.IsBeingTired && !ow.IsBeingBoss())
                workersSelected.Add(ow);
        while (workers.Count - workersSelected.Count < fireCount.workers)
        {
            worker = workersSelected[Random.Range(0, workersSelected.Count)];
            worker.IsBeingTired = true;
            workersSelected.Remove(worker);
        }
        
        //while (workers.Count(x => x.IsBeingTired && !x.isBeingBoss) < fireCount[fireLevel][0]) 
        //{
        //    workersSelected = workers.FindAll(x => !x.IsBeingTired && !x.isBeingBoss);
        //    workersSelected[Random.Range(0, workersSelected.Count)].SetTired();
        //}
        //while (workers.Count(x => x.IsBeingTired && x.isBeingBoss) != fireCount[fireLevel][1]) 
        //{
        //    workersSelected = workers.FindAll(x => !x.IsBeingTired && x.isBeingBoss);
        //    workersSelected[Random.Range(0, workersSelected.Count)].SetTired();
        //}
    }

    private void UpdateFireCount() 
    {
        switch (fireCount.level) 
        {
            case 1:
                fireCount.workers = 3;
                fireCount.bosses = 0;
                fireCount.time = 20;
                break;
            case 2:
                fireCount.workers = 5;
                fireCount.bosses = 1;
                fireCount.time = 20;
                break;
            case 3:
                fireCount.workers = 5;
                fireCount.bosses = 2;
                fireCount.time = 15;
                break;
        }
    }
}
