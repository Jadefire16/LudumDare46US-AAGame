using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    const string manager = "Manager";
    const string deaths = "Deaths";
    const string difficulty = "Difficulty";

    public static string playerName;

    public static GameManager instance;

    int playerDeaths = 0;
    Difficulty gameDifficulty;
    GameState gameState;
    internal static float killLimitY = -50f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(this);
        if (playerName == null)
            playerName = FindObjectOfType<PlayerClass>().name;
    }

    private void Start()
    {
        EventManager.instance.OnPlayerDeathEvent += OnPlayerDeath;
        EventManager.instance.OnSaveGameEvent += Save;
        Load();
    }

    private void Save()
    {
        SaveManager.instance.SaveData(manager + deaths, playerDeaths);
        SaveManager.instance.SaveData(manager + difficulty, (int)gameDifficulty);
    }
    private void Load()
    {
        playerDeaths = SaveManager.instance.LoadInt(manager + deaths);
        gameDifficulty = (Difficulty)SaveManager.instance.LoadInt(manager + difficulty);

        if (SceneManager.GetActiveScene().name == "Menu")
            gameState = GameState.Menu; // will search to see if the active scene is named "Menu" if it is, it will set gamestate to menu
        else
            gameState = GameState.Active;
    }

    private void OnPlayerDeath()
    {
        playerDeaths++;
    }

    public void ToggleGameState(bool isActive)
    {
        gameState = isActive ? GameState.Active : GameState.Paused;
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0.01f;
                break;
            case GameState.Active:
                Time.timeScale = 1f;
                break;
            default:
                break;
        }
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

    public int PlayerDeaths { get => playerDeaths; set => playerDeaths = value; }
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