using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Контроллер аудио в боссфайте
/// </summary>
public class BossFightAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource soundsSource;
    [SerializeField]
    private AudioSource fireSource;
    [SerializeField]
    private AudioSource smokeSource;


    [SerializeField]
    private AudioClip winClip;
    [SerializeField]
    private AudioClip[] failClips;

    private void Start()
    {
        BossFightController.onStartFight += OnStartFight;
        BossFightController.onEndFight += OnEndFight;
    }

    private void OnDestroy()
    {
        BossFightController.onStartFight -= OnStartFight;
        BossFightController.onEndFight -= OnEndFight;
    }

    private void OnStartFight()
    {
        fireSource.Play();
        BossFightHandler.onPlayerPress += OnPlayerPress;
    }

    private void OnEndFight(bool isSuccess)
    {
        fireSource.Stop();

        if (isSuccess)
        {
            smokeSource.Play();
            soundsSource.PlayOneShot(winClip);
        }

        BossFightHandler.onPlayerPress -= OnPlayerPress;
    }

    private void OnPlayerPress(bool IsSuccess)
    {
        if (!IsSuccess)
        {
            int rand = Random.Range(0, failClips.Length);
            soundsSource.PlayOneShot(failClips[rand]);
        }
    }
}
