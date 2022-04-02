using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер босс-файта
/// </summary>
public class BossFightController : MonoBehaviour
{
    /// <summary>
    /// Начало файта
    /// </summary>
    public static event Action onStartFight = delegate { };

    /// <summary>
    /// Конец файта.
    /// true - победа, false - поражение
    /// </summary>
    public static event Action<bool> onEndFight = delegate { };

    /// <summary>
    /// Смена времени файта.
    /// int - сколько времени осталось
    /// </summary>
    public static event Action<int> onTimeChange = delegate { };

    /// <summary>
    /// Секунд на файт
    /// </summary>
    public int SecondsToFight => secondsToFight;

    [SerializeField]
    [Min(1)]
    [Header("Количество удачных нажатий для победы")]
    private int targetSuccessCount = 1;

    [SerializeField]
    [Min(0)]
    [Header("Максимум неудачных попыток")]
    private int maxFailCount;

    [SerializeField]
    [Min(0)]
    [Header("Секунд на файт")]
    private int secondsToFight = 5;

    private int currSuccessCount = 0;
    private int currFailCount = -1;
    private int secondsLeft;

    private Coroutine timer;

    private void Start()
    {
        BossFightHandler.onPlayerPress += OnPlayerPress;
    }

    private void OnDestroy()
    {
        BossFightHandler.onPlayerPress -= OnPlayerPress;
    }

    /// <summary>
    /// Начать босс-файт
    /// </summary>
    public void StartBossFight()
    {
        if (timer != null)
        {
            Debug.LogError("Битва с боссом уже начата!");
            return;
        }

        currSuccessCount = 0;
        currFailCount = -1;

        timer = StartCoroutine(Timer());

        onStartFight();
    }

    /// <summary>
    /// Начать босс-файт c заданными параметрами
    /// </summary>
    /// <param name="targetSuccessCount"> Количество удачных нажатий для победы </param>
    /// <param name="maxFailCount"> Максимум неудачных попыток </param>
    /// <param name="secondsToFight"> Секунд на файт </param>
    public void StartBossFight(int targetSuccessCount, int maxFailCount, int secondsToFight)
    {
        this.targetSuccessCount = targetSuccessCount;
        this.maxFailCount = maxFailCount;
        this.secondsToFight = secondsToFight;

        StartBossFight();
    }

    private IEnumerator Timer()
    {
        secondsLeft = secondsToFight;

        while (enabled)
        {
            yield return new WaitForSeconds(1f);

            secondsLeft--;
            onTimeChange(secondsLeft);

            if (secondsLeft <= 0)
            {
                EndFight(false);
            }
        }
    }

    private void OnPlayerPress(bool isSuccess)
    {
        if (timer == null) return;

        if (isSuccess)
        {
            currSuccessCount++;

            if (currSuccessCount >= targetSuccessCount)
            {
                EndFight(true);
            }
        }
        else
        {
            currFailCount++;

            if (currFailCount >= maxFailCount)
            {
                EndFight(false);
            }
        }
    }

    private void EndFight(bool isSuccess)
    {
        StopCoroutine(timer);
        timer = null;
        onEndFight(isSuccess);
    }

    /// <summary>
    /// Досрочно выйти из файта с поражением
    /// </summary>
    public void ForceEndFight()
    {
        if (timer != null)
        {
            StopCoroutine(timer);
            timer = null;
            onEndFight(false);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartBossFight();
        }
    }
#endif
}
