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
    [SerializeField]
    private PlayerInput playerInput;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput.Direction * Time.fixedDeltaTime * speed;
    }
}
