using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightHandler : MonoBehaviour
{
    /// <summary>
    /// Вызывается, когда игрок нажал на клавишу.
    /// true - успех, false - промазал.
    /// </summary>
    public static event Action<bool> onPlayerPress = delegate { };

    [SerializeField]
    private KeyCode key = KeyCode.E;

    private bool inZone;

    private const string INPUT_TAG = "SpriteInput";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == INPUT_TAG)
        {
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == INPUT_TAG)
        {
            inZone = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            onPlayerPress(inZone);
        }
    }
}
