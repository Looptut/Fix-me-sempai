using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialListener : MonoBehaviour
{
    [SerializeField]
    private TweenBase eBtnTween;
    [SerializeField]
    private TweenBase tabTween;
    [SerializeField]
    private float delay = 4f;

    private void Start()
    {
        tabTween.gameObject.SetActive(true);
        TutorialDialog.OnTutorialEnds += OnEnd;
    }

    private void OnDestroy()
    {
        TutorialDialog.OnTutorialEnds -= OnEnd;
    }

    private void OnEnd()
    {
        tabTween.gameObject.SetActive(false);
        eBtnTween.gameObject.SetActive(true);

        Invoke(nameof(Disable), delay);
    }

    private void Disable()
    {
        eBtnTween.gameObject.SetActive(false);
    }
}
