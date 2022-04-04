using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Sprite[] spritesFire;

    private Vector3 position;
    private Vector3 moveDirection;
    private int spriteNum;

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
        Vector3 moveDirection = (transform.position - position).normalized;

        if (moveDirection == new Vector3(-1, 0) || moveDirection == new Vector3(-1, -1))
            spriteNum = 0;
        if (moveDirection == new Vector3(0, 1) || moveDirection == new Vector3(-1, 1))
            spriteNum = 1;
        if (moveDirection == new Vector3(0, -1) || moveDirection == new Vector3(1, -1))
            spriteNum = 2;
        if (moveDirection == new Vector3(1, 0) || moveDirection == new Vector3(1, 1))
            spriteNum = 3;

        spriteRenderer.sprite = sprites[spriteNum];

        //if ((transform.position - position).normalized == new Vector3(0, 1)) spriteRenderer.sprite = sprites[1];
        //if ((transform.position - position).normalized == new Vector3(-1, 1)) spriteRenderer.sprite = sprites[1];
        //if ((transform.position - position).normalized == new Vector3(-1, 0)) spriteRenderer.sprite = sprites[0];
        //if ((transform.position - position).normalized == new Vector3(-1, -1)) spriteRenderer.sprite = sprites[0];
        //if ((transform.position - position).normalized == new Vector3(0, -1)) spriteRenderer.sprite = sprites[2];
        //if ((transform.position - position).normalized == new Vector3(1, -1)) spriteRenderer.sprite = sprites[2];
        //if ((transform.position - position).normalized == new Vector3(1, 0)) spriteRenderer.sprite = sprites[2];
        //if ((transform.position - position).normalized == new Vector3(1, 1)) spriteRenderer.sprite = sprites[3];
    }
    public void GetExtiguisher()
    {
        if (spriteRenderer.sprite == sprites[spriteNum])
            spriteRenderer.sprite = spritesFire[spriteNum];
        else
            spriteRenderer.sprite = sprites[spriteNum];
    }
}
