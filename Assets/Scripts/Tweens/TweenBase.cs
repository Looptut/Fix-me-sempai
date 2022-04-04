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
    [SerializeField]
    protected bool useUnscaledTime = true;

    protected Tweener currTween;

    protected virtual void OnEnable()
    {
        if (playOnEnable)
        {
            Play();
        }
    }

    protected virtual void OnDisable()
    {
        if (currTween != null)
        {
            currTween.Kill();
            currTween = null;
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

    public virtual void Rewind()
    {
        currTween.Rewind();
    }

    protected abstract void PlayOnce();

    protected abstract void PlayPingPong();

    protected abstract void PlayLoop();
}
