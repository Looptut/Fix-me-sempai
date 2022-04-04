using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Worker : MonoBehaviour
{
    public static event Action ATTENTION_BOSS_ON_FIRE = delegate { };

    /// <summary>
    /// Работник сгорел или потух
    /// 1 bool: true - потух, false - сгорел
    /// 2 bool: true - босс, false - обычный работяга
    /// </summary>
    public static Action<bool, bool> OnStateChange = delegate { };

    public UnityEvent ActionDone = new UnityEvent();

    public event Action<Worker> OnAction = delegate { };
    public static Action OnBeingBoss = delegate { };

    public Transform BubblePosition;
    public Transform CheckPoint => checkPoint;

    [SerializeField] private BossFightController bossFightController;
    [SerializeField] private PlayerInput player;
    [SerializeField] private Transform fire;
    [SerializeField] private bool isBeingTired = false;
    [SerializeField] private bool isBeingBoss = false;
    [SerializeField] private Transform checkPoint;
    
    private WorkerTimer timer;

    public bool IsBeingTired
    {
        get => isBeingTired;
        set
        {
            if (isBeingTired != value)
            {
                isBeingTired = value;
                if (isBeingTired)
                {
                    if (IsBeingBoss)
                        ATTENTION_BOSS_ON_FIRE();
                    if (fire != null)
                        fire.gameObject.SetActive(true);

                    if (timer)
                        timer.StartTimer();
                }
            }
        }
    }
    public bool IsBeingBoss => isBeingBoss;

    private void Start()
    {
        bossFightController = FindObjectOfType<BossFightController>();
        timer = GetComponentInChildren<WorkerTimer>();

        if (fire != null)
            fire.gameObject.SetActive(false);
    }


    [ContextMenu("Switch tired")]
    public void SwitchTired()
    {
        IsBeingTired = !IsBeingTired;
    }

    public virtual void PerformAction()
    {
        OnAction.Invoke(this);

        if (IsBeingTired && !IsBeingBoss)
        {
            fire.gameObject.SetActive(false);
            IsBeingTired = false;
            player.DoAction(fire);

            if (timer) timer.StopTimer();
        }
        if (IsBeingTired && IsBeingBoss)
        {

            if (timer) timer.StopTimer();
            BossFightController.onEndFight += OnEndFight;
            bossFightController.StartBossFight();
        }
        ActionDone.Invoke();
    }

    private void OnEndFight(bool isSuccess)
    {
        if (isSuccess)
        {
            fire.gameObject.SetActive(false);
            IsBeingTired = false;
            player.DoAction(fire);
        }
        OnStateChange(isSuccess, isBeingBoss);
        BossFightController.onEndFight -= OnEndFight;


        if (timer) timer.StopTimer();
    }

    /// <summary>
    /// Время таймера вышло
    /// </summary>
    /// </summary>
    public void FailFromTimer()
    {
        IsBeingTired = false;
        fire.gameObject.SetActive(false);
        OnStateChange(false, IsBeingBoss);
    }
}