using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenAlpha : TweenBase
{
    [SerializeField]
    [Range(0, 1)]
    private float start;
    [SerializeField]
    [Range(0, 1)]
    private float end;

    private SpriteRenderer sprite;
    private Image image;

    private Color color;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();

        if (sprite) color = sprite.color;
        if (image) color = image.color;
    }

    protected override void PlayOnce()
    {
        if (sprite)
        {
            Color tmpColor = SetSpriteColor();
            currTween = sprite.DOColor(tmpColor, duration).SetEase(ease);
        }

        if (image)
        {
            Color tmpColor = SetImageColor();
            currTween = image.DOColor(tmpColor, duration).SetEase(ease);
        }
    }

    protected override void PlayPingPong()
    {
        if (sprite)
        {
            Color tmpColor = SetSpriteColor();

            currTween = sprite.DOColor(tmpColor, duration).SetEase(ease).OnComplete(() =>
            {
                tmpColor.a = start;
                currTween = sprite.DOColor(tmpColor, duration).SetEase(ease).OnComplete(() =>
                {
                    Play();
                });
            });
        }

        if (image)
        {
            Color tmpColor = SetImageColor();

            currTween = image.DOColor(tmpColor, duration).SetEase(ease).OnComplete(() =>
            {
                tmpColor.a = start;
                currTween = image.DOColor(tmpColor, duration).SetEase(ease).OnComplete(() =>
                {
                    Play();
                });
            });
        }
    }

    protected override void PlayLoop()
    {
        if (sprite)
        {
            Color tmpColor = SetSpriteColor();

            currTween = sprite.DOColor(tmpColor, duration).SetEase(ease).OnComplete(() =>
            {
                tmpColor.a = start;
                sprite.color = tmpColor;
                Play();
            });
        }

        if (image)
        {
            Color tmpColor = SetImageColor();

            currTween = image.DOColor(tmpColor, duration).SetEase(ease).OnComplete(() =>
            {
                tmpColor.a = start;
                image.color = tmpColor;
                Play();
            });
        }
    }

    private Color SetSpriteColor()
    {
        Color tmpColor = color;
        tmpColor.a = start;
        sprite.color = tmpColor;
        tmpColor.a = end;
        return tmpColor;
    }

    private Color SetImageColor()
    {
        Color tmpColor = color;
        tmpColor.a = start;
        image.color = tmpColor;
        tmpColor.a = end;
        return tmpColor;
    }
}
