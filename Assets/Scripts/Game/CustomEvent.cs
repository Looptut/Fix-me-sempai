using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "Events/Event")]
public class CustomEvent : ScriptableObject
{
    public event Action OnEvent = delegate { };

    [SerializeField, Multiline] private string description;

    private bool isActive;
    public bool IsActive
    {
        get => isActive;
        private set => isActive = value;
    }

    public bool TryChangeActivityTo(bool isActive)
    {
        if (IsActive == isActive)
        {
            return false;
        }

        IsActive = isActive;
        OnEvent.Invoke();
        return true;
    }
}
