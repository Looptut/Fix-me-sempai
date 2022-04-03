using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class TweenUIPosition : TweenBase
{
    [SerializeField]
    private Vector2 end;
    [SerializeField]
    private bool overrideStart;
    [SerializeField]
    private Vector2 start;

    private Vector2 startPos;

    private RectTransform rect => transform as RectTransform;

    private void Awake()
    {
        if (overrideStart)
        {
            rect.anchoredPosition = start;
        }
        startPos = rect.anchoredPosition;
    }

    protected override void PlayOnce()
    {
        rect.anchoredPosition = startPos;
        currTween = rect.DOAnchorPos(end, duration).SetEase(ease);
    }

    protected override void PlayPingPong()
    {
        rect.anchoredPosition = startPos;
        currTween = rect.DOAnchorPos(end, duration).SetEase(ease).OnComplete(() =>
        {
            currTween = rect.DOAnchorPos(startPos, duration).SetEase(ease).OnComplete(() =>
            {
                Play();
            });
        });
    }

    protected override void PlayLoop()
    {
        rect.anchoredPosition = startPos;
        currTween = rect.DOAnchorPos(end, duration).SetEase(ease).OnComplete(() =>
        {
            Play();
        });
    }

    [ContextMenu("Установить начальную позицию")]
    private void SetStartPosition()
    {
        start = rect.anchoredPosition;
    }

    [ContextMenu("Установить конечную позицию")]
    private void SetEndPosition()
    {
        end = rect.anchoredPosition;
    }
}
