using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    int playerMaxLives = 5;
    int playerDeaths = 0;
    Vector3 currentCheckpoint;
    Difficulty gameDifficulty;
    GameState gameState;
    internal static float killLimitY = -50f;

    public Vector3 CurrentCheckpoint { get => currentCheckpoint; set => currentCheckpoint = value; }
    public int PlayerMaxLives { get => playerMaxLives; }

    private void Awake()
    {
        if (!instance)
            instance = this;
        DontDestroyOnLoad(this);

        Initialize();
    }

    private void Start()
    {
        
    }

    private void Initialize()
    {
        Debug.Log("This is where things would initialize");
        gameState = GameState.Menu;
    }

    public void RestartLevel()
    {
        Debug.Log("This would restart level and set player to checkpoint");
    }

    public Difficulty GameDifficulty
    {
        get => gameDifficulty;
        set
        {
            if (gameState == GameState.Menu) gameDifficulty = value;
            else { return; }
        }
    }
}

public enum Difficulty
{
    VeryEasy = 0,
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Nightmare = 4
}

public enum GameState
{
    Menu,
    Paused,
    Active
}