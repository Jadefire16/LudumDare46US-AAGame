using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public event Action OnPlayerDeathEvent;

    private void Awake()
    {
        if (!instance)
            instance = this;
        DontDestroyOnLoad(this);
    }

    public void CallPlayerDeath() => OnPlayerDeathEvent?.Invoke();
}
