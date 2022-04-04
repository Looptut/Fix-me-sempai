using System;
using System.Collections;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnInputAction = delegate { };

    [SerializeField] private KeyCode keyInput = KeyCode.E;
    [SerializeField] private CustomEvent inputEvent;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerView view;

    [Space]
    [Header("Action")]
    [SerializeField] private float extinguisherAwait = 2f;
    [SerializeField] private ParticleSystem extinguisher;

    private BossFightController bossFightController;
    private Coroutine coroutine;

    private void Start()
    {
        bossFightController = FindObjectOfType<BossFightController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyInput) && bossFightController.CanStartFight && coroutine == null)
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
        view.GetExtiguisher();
        extinguisher.transform.position = fire.position;
        extinguisher.gameObject.SetActive(true);
        fire.gameObject.SetActive(false);
        yield return new WaitForSeconds(extinguisherAwait);
        extinguisher.gameObject.SetActive(false);
        movement.CanMove = true;
        view.GetExtiguisher();
        coroutine = null;
    }
}