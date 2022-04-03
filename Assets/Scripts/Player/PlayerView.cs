using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    private Vector3 position;
    private Vector3 moveDirection;

    private void Awake()
    {
        position = transform.position;
        moveDirection = Vector2.zero;
    }

    private void Update()
    {
        if (moveDirection != (transform.position - position).normalized)
        {
            ChangeSprite();
            moveDirection = (transform.position - position).normalized;
        }
        position = transform.position;
    }
    private void ChangeSprite()
    {
        if ((transform.position - position).normalized == new Vector3(0, 1)) spriteRenderer.sprite = sprites[1];
        if ((transform.position - position).normalized == new Vector3(-1, 1)) spriteRenderer.sprite = sprites[1];
        if ((transform.position - position).normalized == new Vector3(-1, 0)) spriteRenderer.sprite = sprites[0];
        if ((transform.position - position).normalized == new Vector3(-1, -1)) spriteRenderer.sprite = sprites[0];
        if ((transform.position - position).normalized == new Vector3(0, -1)) spriteRenderer.sprite = sprites[2];
        if ((transform.position - position).normalized == new Vector3(1, -1)) spriteRenderer.sprite = sprites[2];
        if ((transform.position - position).normalized == new Vector3(1, 0)) spriteRenderer.sprite = sprites[2];
        if ((transform.position - position).normalized == new Vector3(1, 1)) spriteRenderer.sprite = sprites[3];
    }
}
