using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumbleDialogs : MonoBehaviour
{
    public List<string> speeches;
    public List<Worker> workers;

    [SerializeField] private DialogBubble bubble;
    [SerializeField] private float viewDuration = 3f;
    [SerializeField] private float delay = 5f;
    private System.Random random;

    private Coroutine coroutine;

    private void Start()
    {
        random = new System.Random();
        TutorialDialog.OnTutorialEnds += SetDialogs;
    }

    private void SetDialogs()
    {
        coroutine = StartCoroutine(RandomSpeeches());
    }

    private IEnumerator RandomSpeeches()
    {
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (enabled)
        {
            yield return wait;
            SetSpeech();
        }
    }

    public void SetSpeech()
    {
        bubble.transform.position = workers[random.Next(0, workers.Count)].BubblePosition.position;
        bubble.SetText(speeches[random.Next(0, speeches.Count)]);
        bubble.gameObject.SetActive(true);
        Invoke(nameof(DeactivateSpeech), viewDuration);
    }

    private void DeactivateSpeech()
    {
        bubble.gameObject.SetActive(false);
    }
}