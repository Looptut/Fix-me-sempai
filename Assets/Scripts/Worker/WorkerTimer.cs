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
    [SerializeField]
    private Image backImage;

    private Image iconTimer;

    private Worker worker;

    private Coroutine timer;

    private float currTime = 0f;

    private void Awake()
    {
        worker = GetComponentInParent<Worker>();
        iconTimer = GetComponent<Image>();

        iconTimer.enabled = false;
        backImage.enabled = false;
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
        iconTimer.enabled = true;
        backImage.enabled = true;
        iconTimer.color = Color.green;
        iconTimer.fillAmount = 1;
        currTime = 0;
        timer = StartCoroutine(Timer());
    }

    /// <summary>
    /// Остановить отсчёт
    /// </summary>
    public void StopTimer()
    {
        iconTimer.enabled = false;
        backImage.enabled = false;

        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
        }
    }

    private IEnumerator Timer()
    {
        while (enabled)
        {
            yield return null;

            currTime += Time.deltaTime;

            iconTimer.fillAmount = 1 - currTime / maxTimeToFire;

            iconTimer.color = iconTimer.fillAmount <= 0.33f ? Color.red : Color.green;

            if (currTime >= maxTimeToFire)
            {
                worker.FailFromTimer();
                StopTimer();
            }
        }
    }
}
