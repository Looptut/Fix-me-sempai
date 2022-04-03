using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputAction;

    [SerializeField] private KeyCode keyInput = KeyCode.E;
    [SerializeField] private CustomEvent inputEvent;
    [SerializeField] private PlayerMovement movement;

    [Space]
    [Header("Action")]
    [SerializeField] private float extinguisherAwait = 2f;
    [SerializeField] private ParticleSystem extinguisher;

    private Coroutine coroutine;

    private void Update()
    {
        if (Input.GetKeyDown(keyInput) && inputEvent.IsActive)
        {
            OnInputAction.Invoke();
        }
    }

    public void DoAction(Transform fire)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Extinguisher(fire));
        }
    }

    private IEnumerator Extinguisher(Transform fire)
    {
        movement.CanMove = false;
        extinguisher.transform.position = fire.position;
        extinguisher.gameObject.SetActive(true);
        fire.gameObject.SetActive(false);
        yield return new WaitForSeconds(extinguisherAwait);
        extinguisher.gameObject.SetActive(false);
        movement.CanMove = true;
        coroutine = null;
    }
}