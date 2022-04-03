using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Передвижение игрока
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 250f;

    private Rigidbody2D rb;
    private Vector2 input;
    public Vector2 Input => input;

    public bool CanMove = true;

    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        BossFightController.onStartFight += OnStartFight;
        BossFightController.onEndFight += OnEndFight;
    }

    private void OnDestroy()
    {
        BossFightController.onStartFight -= OnStartFight;
        BossFightController.onEndFight -= OnEndFight;
    }

    private void Update()
    {
        input.x = UnityEngine.Input.GetAxisRaw(HORIZONTAL);
        input.y = UnityEngine.Input.GetAxisRaw(VERTICAL);
    }

    private void FixedUpdate()
    {
        rb.velocity = CanMove ? input * Time.fixedDeltaTime * speed : Vector2.zero;
    }

    private void OnStartFight()
    {
        CanMove = false;
    }

    private void OnEndFight(bool isSuccess)
    {
        CanMove = true;
    }
}
