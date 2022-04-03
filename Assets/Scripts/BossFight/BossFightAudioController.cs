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
    private AudioSource musicSource;

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
        musicSource.Play();
        BossFightHandler.onPlayerPress += OnPlayerPress;
    }

    private void OnEndFight(bool isSuccess)
    {
        musicSource.Stop();

        if (isSuccess)
        {
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
