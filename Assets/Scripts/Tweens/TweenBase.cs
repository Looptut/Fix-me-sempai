using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TweenType
{
    Once,
    PingPong,
    Loop
}

public abstract class TweenBase : MonoBehaviour
{
    [SerializeField]
    protected bool playOnEnable;
    [SerializeField]
    protected TweenType type;
    [SerializeField]
    protected Ease ease = Ease.Linear;
    [SerializeField]
    protected float duration;

    protected virtual void OnEnable()
    {
        if (playOnEnable)
        {
            Play();
        }
    }

    public virtual void Play()
    {
        switch (type)
        {
            case TweenType.Once:
                PlayOnce();
                break;
            case TweenType.PingPong:
                PlayPingPong();
                break;
            case TweenType.Loop:
                PlayLoop();
                break;
            default:
                PlayOnce();
                break;
        }
    }

    protected abstract void PlayOnce();

    protected abstract void PlayPingPong();

    protected abstract void PlayLoop();
}
