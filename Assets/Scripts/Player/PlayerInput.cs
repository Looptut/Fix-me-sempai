using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputAction;

    [SerializeField] private KeyCode keyInput = KeyCode.E;
    [SerializeField] private CustomEvent inputEvent;

    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";

    private Vector2 input;
    public Vector2 Direction => input;
    private void Update()
    {
        input.x = Input.GetAxisRaw(HORIZONTAL);
        input.y = Input.GetAxisRaw(VERTICAL);

        if (Input.GetKeyDown(keyInput) && inputEvent.IsActive)
        {
            OnInputAction.Invoke();
        }
    }
}