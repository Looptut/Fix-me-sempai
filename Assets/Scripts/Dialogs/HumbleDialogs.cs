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

    [SerializeField]
    private AudioSource source;
    [SerializeField] private AudioClip clip;

    private System.Random random;

    private Coroutine coroutine;

    private void Awake()
    {
        workers = new List<Worker>(FindObjectsOfType<Worker>());
    }

    private void Start()
    {
        random = new System.Random();
        TutorialDialog.OnTutorialEnds += SetDialogs;
        ProgressBar.onProgressEnd += HandledeactivateProgress;
    }

    private void HandledeactivateProgress(bool obj)
    {
        StopCoroutine(coroutine);
        bubble.gameObject.SetActive(false);
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
        if (source && clip != null)
            source.PlayOneShot(clip);
        Invoke(nameof(DeactivateSpeech), viewDuration);
    }

    private void DeactivateSpeech()
    {
        bubble.gameObject.SetActive(false);
    }
}