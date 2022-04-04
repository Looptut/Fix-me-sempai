using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    void Start()
    {
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
        BossFightController.onStartFight += HandleAudio;
        BossFightController.onEndFight += HandleUnMute;
    }
}
