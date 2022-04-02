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

    private bool canMove = true;

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
        input.x = Input.GetAxisRaw(HORIZONTAL);
        input.y = Input.GetAxisRaw(VERTICAL);
    }

    private void FixedUpdate()
    {
        rb.velocity = canMove ? input * Time.fixedDeltaTime * speed : Vector2.zero;
    }

    private void OnStartFight()
    {
        canMove = false;
    }

    private void OnEndFight(bool isSuccess)
    {
        canMove = true;
    }
}
