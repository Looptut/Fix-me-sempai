using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPosition : TweenBase
{
    [SerializeField]
    private Vector3 end;
    [SerializeField]
    private bool overrideStart;
    [SerializeField]
    private Vector3 start;

    private Vector3 startPos;

    private void Awake()
    {
        if (overrideStart)
        {
            transform.localPosition = start;
        }
        startPos = transform.localPosition;
    }

    protected override void PlayOnce()
    {
        transform.localPosition = startPos;
        currTween = transform.DOLocalMove(end, duration).SetEase(ease).SetUpdate(useUnscaledTime);
    }

    protected override void PlayPingPong()
    {
        transform.localPosition = startPos;
        currTween = transform.DOLocalMove(end, duration).SetEase(ease).SetUpdate(useUnscaledTime).OnComplete(() =>
        {
            currTween = transform.DOLocalMove(startPos, duration).SetEase(ease).SetUpdate(useUnscaledTime).OnComplete(() =>
            {
                Play();
            });
        });
    }

    protected override void PlayLoop()
    {
        transform.localPosition = startPos;
        currTween = transform.DOLocalMove(end, duration).SetEase(ease).SetUpdate(useUnscaledTime).OnComplete(() =>
        {
            Play();
        });
    }
}
