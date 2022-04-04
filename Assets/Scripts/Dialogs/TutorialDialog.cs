using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialDialog : MonoBehaviour
{
    public UnityEvent OnShowExtinguisher;

    [SerializeField] private KeyCode skipTutorial = KeyCode.Tab;

    public static event Action OnTutorialEnds = delegate { };

    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Transform president;
    [SerializeField] private DialogBubble bubble;
    [SerializeField] private float waitStartTutorial = 2f;
    [SerializeField] private float waitNextText = 5f;
    [SerializeField] private int extinguisherIndex = 5;
    public List<string> speeches;

    private Coroutine coroutine;

    [SerializeField]
    private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        coroutine = StartCoroutine(Tutorial());
    }

    [ContextMenu("Skip tutorial")]
    public void SkipTutorial()
    {

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        bubble.gameObject.SetActive(false);
        movement.CanMove = true;
        OnTutorialEnds.Invoke();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(skipTutorial))
        {
            SkipTutorial();
        }
    }

    private IEnumerator Tutorial()
    {
        movement.CanMove = false;
        WaitForSeconds wait = new WaitForSeconds(waitNextText);
        yield return new WaitForSeconds(waitStartTutorial);
        bubble.transform.position = president.position;
        bubble.SetText(speeches[0]);
        bubble.gameObject.SetActive(true);
        if (source && clip != null)
            source.PlayOneShot(clip);
        for (int i = 1; i < speeches.Count; i++)
        {
            yield return wait;
            bubble.SetText(speeches[i]);
            if (source && clip != null)
                source.PlayOneShot(clip);
            if (i == extinguisherIndex)
            {
                OnShowExtinguisher.Invoke();
            }
        }
        yield return wait;
        bubble.gameObject.SetActive(false);
        movement.CanMove = true;
        OnTutorialEnds.Invoke();
        coroutine = null;
        gameObject.SetActive(false);
    }
}
