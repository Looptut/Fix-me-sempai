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

    private const string VERTICAL = "Vertical";
    private const string HORIZONTAL = "Horizontal";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw(HORIZONTAL);
        input.y = Input.GetAxisRaw(VERTICAL);
    }

    private void FixedUpdate()
    {
        rb.velocity = input * Time.fixedDeltaTime * speed;
    }
}
