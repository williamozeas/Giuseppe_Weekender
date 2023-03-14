using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    GameEnd,
    BetweenLevels,
    Pause,
    Menu
}

public enum SceneNum
{
    MainMenu,
    Level1,
    Level2,
    Level3
}

public class GameManager : Singleton<GameManager>
{
    [Header("Game Params")]
    [SerializeField] private float maxTime = 30f;

    public float MaxTime => maxTime;
    
    [Header("Data")]
    private GameState _gamestate;
    public GameState GameState //GameState cannot be set without calling SetGameState
    {
        set { SetGameState(value); }
        get { return _gamestate; }
    }
    private SceneNum _currentScene;
    public SceneNum CurrentScene //GameState cannot be set without calling SetGameState
    {
        set { LoadLevel(value); }
        get { return _currentScene; }
    }
    
    //Events
    public static event Action OnGameStart;
    public static event Action OnGameOver;
    public static event Action OnDie;
    public static event Action OnRunOutOfTime;
    
    //References (Should set themselves in their Awake() functions)
    private Player _player;
    public Player Player => _player;
    private RewindManager _rewindManager;
    public RewindManager RewindManager => _rewindManager;
    
    private WorldTimer _timer;
    public WorldTimer Timer => _timer;
    public float Time => _timer.Time;
    
    public override void Awake()
    {
        _rewindManager = GetComponent<RewindManager>();
        _timer = GetComponent<WorldTimer>();
    }
    
    public void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case (GameState.Menu):
            {
                //TODO: reset variables
                LoadLevel(SceneNum.MainMenu);
                break;
            }
            case (GameState.Playing):
            {
                OnGameStart?.Invoke();
                break;
            }
            case (GameState.GameEnd):
            {
                OnGameOver?.Invoke();
                break;
            }
        }
        _gamestate = newGameState;
    }
    
    public void LoadLevel(SceneNum newScene)
    {
        switch (newScene)
        {
            case (SceneNum.MainMenu):
            {
                //load scene level
                break;
            }
            case (SceneNum.Level1):
            {
                //load level 1
                break;
            }
            case (SceneNum.Level2):
            {
                //load level 2
                break;
            }
            case (SceneNum.Level3):
            {
                //load level 3
                break;
            }
        }
        _currentScene = newScene;
    }

    public void SetPlayer(Player newPlayer)
    {
        _player = newPlayer;
    }

    public static void Die()
    {
        OnDie?.Invoke();
    }

    public static void RunOutOfTime()
    {
        OnRunOutOfTime?.Invoke();
    }
}
