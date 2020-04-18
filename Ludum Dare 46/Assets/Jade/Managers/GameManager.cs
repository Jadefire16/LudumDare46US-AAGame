using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int playerMaxLives = 5;
    int playerDeaths = 0;
    Vector3 currentCheckpoint;

    public Vector3 CurrentCheckpoint { get => currentCheckpoint; set => currentCheckpoint = value; }
    public int PlayerMaxLives { get => playerMaxLives; }

    private void Awake()
    {
        if (!instance)
            instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Debug.Log("This is where things would initialize");
    }

    public void RestartLevel()
    {
        Debug.Log("This would restart level and set player to checkpoint");
    }
}
