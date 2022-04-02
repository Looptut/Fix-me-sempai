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
        transform.DOLocalMove(end, duration).SetEase(ease);
    }

    protected override void PlayPingPong()
    {
        transform.DOLocalMove(end, duration).SetEase(ease).OnComplete(() =>
        {
            transform.DOLocalMove(startPos, duration).SetEase(ease).OnComplete(() =>
            {
                Play();
            });
        });
    }

    protected override void PlayLoop()
    {
        transform.DOLocalMove(end, duration).SetEase(ease).OnComplete(() =>
        {
            transform.localPosition = startPos;
            Play();
        });
    }
}
