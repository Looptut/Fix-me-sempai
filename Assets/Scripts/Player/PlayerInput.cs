using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputAction;

    [SerializeField] private KeyCode keyInput = KeyCode.E;
    [SerializeField] private CustomEvent inputEvent;

    private void Update()
    {
        if (Input.GetKeyDown(keyInput) && inputEvent.IsActive)
        {
            OnInputAction.Invoke();
        }
    }
}