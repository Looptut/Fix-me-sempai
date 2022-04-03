using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct Level
{
    [Header("Пройдено время с начала игры для этого уровня")]
    public int LevelTime;
    [Header("Интервал для спавна работников")]
    public int Interval;
    [Header("Работников возгарает каждый интервал")]
    public int WorkersCount;
}

public class FireControl : MonoBehaviour
{
    [SerializeField]
    [Header("Время появление каждого босса")]
    private List<int> bossTimer = new List<int>() { 10, 20, 30 };
    [SerializeField]
    [Header("Уровни")]
    private List<Level> levels;

    private List<Worker> workers;
    private List<Worker> bosses;

    private float startTime;
    private float lastWorkerFire = 0f;

    private int currLevelIndex = 0;

    private void Start()
    {
        FindWorkers();

        startTime = Time.time;

        StartControl();
    }

    private void FindWorkers()
    {
        workers = new List<Worker>(FindObjectsOfType<Worker>().Where((c) => !c.IsBeingBoss));
        bosses = new List<Worker>(FindObjectsOfType<Worker>().Where((c) => c.IsBeingBoss));
    }

    public void StartControl()
    {
        StartCoroutine(Control());
    }

    private IEnumerator Control()
    {
        while (enabled)
        {
            CheckLevel();
            SpawnWorkerFire();
            SpawnBossFire();

            yield return null;
        }
    }

    private void CheckLevel()
    {
        for (int i = currLevelIndex; i < levels.Count; i++)
        {
            if (Time.time - startTime >= levels[i].LevelTime)
            {
                currLevelIndex = i;
            }
        }
    }

    private void SpawnWorkerFire()
    {
        if (lastWorkerFire + levels[currLevelIndex].Interval > Time.time - startTime) return;

        int spawnCount = Math.Min(levels[currLevelIndex].WorkersCount, workers.Count);
        Debug.Log($"WORKERS: {spawnCount}");
        for (int i = 0; i < spawnCount; i++)
        {
            int rand = Random.Range(0, workers.Count);
            Worker worker = workers[rand];
            worker.IsBeingTired = true;

            workers.Remove(worker);
            worker.OnAction += ReturnWorkerToPool;
        }
        lastWorkerFire = Time.time;
    }

    private void SpawnBossFire()
    {
        for (int i = 0; i < bossTimer.Count; i++)
        {
            if (Time.time - startTime > bossTimer[i])
            {
                Debug.Log($"BOSS: {bossTimer[i]}");
                int rand = Random.Range(0, bosses.Count);
                Worker boss = bosses[rand];
                boss.IsBeingTired = true;
                bosses.Remove(boss);
                bossTimer.RemoveAt(i);
                i--;
            }
        }
    }

    private void ReturnWorkerToPool(Worker worker)
    {
        worker.OnAction -= ReturnWorkerToPool;
        workers.Add(worker);
    }

    /*
    private const string WORKER_TAG = "Worker";
    private List<Worker> workers = new List<Worker>();
    private List<Worker> workersSelected = new List<Worker>();
    private Worker worker;
    private FireCount maxFireCount;
    private float timeCount = 0;
    private float spawnTime = 0;

    public FireCount fireCount;
    public ProgressBar progressBar;
    public int spawnInterval;

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
        fireCount = new FireCount() { level = 1, workers = 0, bosses = 0, time = 0 }; 
        maxFireCount = new FireCount();
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
        InvokeRepeating(nameof(setFire), 0, 1.0f);
    }
    void Update()
    {
        timeCount += Time.deltaTime;
    }

    private void setFire() 
    {
        int points = progressBar.GetCurrentPoints();
        if (points < 30) maxFireCount.level = 1;
        if (points > 29 && points < 70) maxFireCount.level = 2;
        if (points > 69) maxFireCount.level = 3;

        UpdateMaxFireCount();

        if (timeCount - spawnTime > spawnInterval)        
            SpawnFire();
        

        workersSelected = new List<Worker>();
        foreach (Worker ow in workers)
            if (!ow.IsBeingTired && !ow.IsBeingBoss)
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

    private void SpawnFire() 
    {
        spawnTime += spawnInterval;

        workersSelected = new List<Worker>();
        if (fireCount.workers < maxFireCount.workers)
        {
            foreach (Worker ow in workers)
                if (!ow.IsBeingTired && !ow.IsBeingBoss)
                    workersSelected.Add(ow);
            workersSelected[Random.Range(0, workersSelected.Count)].IsBeingTired = true;
            fireCount.workers++;
        }
        else if (fireCount.bosses < maxFireCount.bosses)
        {
            foreach (Worker ow in workers)
                if (!ow.IsBeingTired && ow.IsBeingBoss)
                    workersSelected.Add(ow);
            workersSelected[Random.Range(0, workersSelected.Count)].IsBeingTired = true;
            fireCount.bosses++;
        }
    }

    private void UpdateMaxFireCount() 
    {
        switch (maxFireCount.level) 
        {
            case 1:
                maxFireCount.workers = 3;
                maxFireCount.bosses = 0;
                maxFireCount.time = 20;
                break;
            case 2:
                maxFireCount.workers = 5;
                maxFireCount.bosses = 1;
                maxFireCount.time = 20;
                break;
            case 3:
                maxFireCount.workers = 5;
                maxFireCount.bosses = 2;
                maxFireCount.time = 15;
                break;
        }
    }
    */
}
