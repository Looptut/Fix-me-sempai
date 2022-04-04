using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Таймер горения работника
/// </summary>
[RequireComponent(typeof(Image))]
public class WorkerTimer : MonoBehaviour
{
    [SerializeField]
    [Header("Время горения")]
    private float maxTimeToFire = 10f;

    private Image iconTimer;

    private Worker worker;

    private Coroutine timer;

    private float currTime = 0f;

    private void Awake()
    {
        worker = GetComponentInParent<Worker>();
        iconTimer = GetComponent<Image>();
    }

    /// <summary>
    /// Начать отсчёт
    /// </summary>
    public void StartTimer()
    {
        if (timer != null)
        {
            Debug.LogError("Попытка запустить уже идущий таймер");
            return;
        }

        currTime = 0;
        timer = StartCoroutine(Timer());
    }

    /// <summary>
    /// Остановить отсчёт
    /// </summary>
    public void StopTimer()
    {
        StopCoroutine(timer);
        timer = null;
    }

    private IEnumerator Timer()
    {
        while (enabled)
        {
            yield return null;

            currTime += Time.deltaTime;

            iconTimer.fillAmount = currTime / maxTimeToFire;

            if (currTime >= maxTimeToFire)
            {
                worker.FailFromTimer();
                StopTimer();
            }
        }
    }
}
