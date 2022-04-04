using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbiController : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();

        BossFightController.onStartFight += HandleAudio;
        BossFightController.onEndFight += HandleUnMute;
    }

    private void HandleUnMute(bool obj)
    {
        source.mute = false;
    }

    private void HandleAudio()
    {
        source.mute = true;
    }

    private void OnDestroy()
    {
        BossFightController.onStartFight -= HandleAudio;
        BossFightController.onEndFight -= HandleUnMute;
    }
}
