using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScale : TweenBase
{
    [SerializeField]
    private float end;
    [SerializeField]
    private bool overrideStart;
    [SerializeField]
    private float start;

    private float startScale;

    private void Awake()
    {
        if (overrideStart)
        {
            transform.localScale = Vector3.one * start;
        }
        startScale = transform.localScale.x;
    }

    protected override void PlayOnce()
    {
        transform.localScale = Vector3.one * startScale;
        currTween = transform.DOScale(end, duration).SetEase(ease);
    }

    protected override void PlayPingPong()
    {
        transform.localScale = Vector3.one * startScale;
        currTween = transform.DOScale(end, duration).SetEase(ease).OnComplete(() =>
        {
            currTween = transform.DOScale(startScale, duration).SetEase(ease).OnComplete(() =>
            {
                Play();
            });
        });
    }

    protected override void PlayLoop()
    {
        transform.localScale = Vector3.one * startScale;
        currTween = transform.DOScale(end, duration).SetEase(ease).OnComplete(() =>
        {
            Play();
        });
    }
}
