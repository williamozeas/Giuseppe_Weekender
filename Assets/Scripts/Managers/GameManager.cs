using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Kitchen,
    Engine,
    Captain
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

    [Header("Scenes")] 
    [SerializeField] private Scene MainMenuScene;
    [SerializeField] private Scene KitchenScene;
    [SerializeField] private Scene EngineScene;
    [SerializeField] private Scene CaptainScene;
    
    //Events
    public static event Action OnGamePlay;
    public static event Action OnGameOver;
    public static event Action<SceneNum> OnLoadScene;
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
                OnGamePlay?.Invoke();
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
                SceneManager.LoadScene(MainMenuScene.buildIndex);
                break;
            }
            case (SceneNum.Kitchen):
            {
                SceneManager.LoadScene(KitchenScene.buildIndex);
                break;
            }
            case (SceneNum.Engine):
            {
                SceneManager.LoadScene(EngineScene.buildIndex);
                break;
            }
            case (SceneNum.Captain):
            {
                SceneManager.LoadScene(CaptainScene.buildIndex);
                break;
            }
        }
        _currentScene = newScene;
        OnLoadScene?.Invoke(newScene);
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
