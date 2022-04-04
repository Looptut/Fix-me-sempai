using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Image image;
    private float targetProgress = 0;
    private int currentPoints = 1;

    public int startPoints;
    public int maxPoints;
    public int workerPointsUp;
    public int bossPointsUp;
    public int workerPointsDown;
    public int bossPointsDown;
    public static event Action<bool> onProgressEnd = delegate { };

    public int GetCurrentPoints() { return currentPoints; }

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        image = gameObject.GetComponentInChildren<Image>().GetComponentInChildren<Image>();
        currentPoints = startPoints;
    }
    void Start()
    {
        ChangeProgress(startPoints);
        Worker.OnStateChange += WorkerChanged;
    }

    private void OnDestroy()
    {
        Worker.OnStateChange -= WorkerChanged;
    }
    void Update()
    {
        if (currentPoints == 0)
        {
            onProgressEnd(false);
            gameObject.SetActive(false);
        }

        if (currentPoints == maxPoints)
        {
            onProgressEnd(true);
            gameObject.SetActive(false);
        }
    }

    private void WorkerChanged(bool result, bool boss) 
    {
        if (result && boss) ChangeProgress(bossPointsUp);
        if (result && !boss) ChangeProgress(workerPointsUp);
        if (!result && boss) ChangeProgress(-bossPointsDown);
        if (!result && !boss) ChangeProgress(-workerPointsDown);
    }

    public void ChangeProgress(int progressValue)
    {
        if ((currentPoints + progressValue) > maxPoints)
            progressValue = maxPoints - currentPoints;
        if ((currentPoints + progressValue) < 0)
            progressValue = -currentPoints;
        currentPoints += progressValue;
        image.fillAmount = (float)currentPoints / maxPoints;
    }
}
