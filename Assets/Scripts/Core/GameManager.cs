﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Pregame,
        Running
    }
    public event Action PlayerSpawned;
    public event Action<int> LivesUpdated;
    public event Action<GameState, GameState> GameStateChanged;

    private static Camera _mainCamera;

    public PlayerController player;
    public int playerLives;
    public int startingLevel = 0;

    public GameObject levelTransition;

    private int _currentLevelIndex;
    private PlayerController _playerController;
    private int _currentPlayerLives;
    private GameObject _playerStart;

    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    public int CurrentLevelIndex
    {
        get => _currentLevelIndex;
        set
        {
            _currentLevelIndex = value;
        }
    }

    public int CurrentPlayerLives
    {
        get => _currentPlayerLives;
        set
        {
            _currentPlayerLives = value;
            LivesUpdated?.Invoke(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = FindObjectOfType<Camera>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        UpdateState(GameState.Pregame);
        LoadLevel(startingLevel);
    }

    public void LoadLevel(int index = 0)
    {
        CurrentPlayerLives = playerLives;
        CurrentLevelIndex = index;
        SceneManager.Instance.LoadLevel(index, levelTransition.GetComponent<Animator>());
        SceneManager.Instance.LevelLoaded += InitializeLevel;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateState(GameState newState)
    {
        var previousState = CurrentGameState;
        CurrentGameState = newState;
        switch (newState)
        {
            case GameState.Pregame:
                break;
            case GameState.Running:
                break;
            default:
                break;
        }
        GameStateChanged?.Invoke(previousState, newState);
    }

    private void InitializeLevel()
    {
        var level = SceneManager.Instance.levelManager.levels[CurrentLevelIndex];
        if (level.levelType != Level.LevelType.Gameplay) return;
        UpdateState(GameState.Running);
        if (_playerController) return;
        StartCoroutine(SpawnPlayer(false));
    }

    private void OnPlayerSpawned(PlayerController playerController)
    {
        SetupFollowCamera(playerController.gameObject, SceneManager.Instance.levelManager.levels[CurrentLevelIndex].levelSize);
    }

    private void SetupFollowCamera(GameObject target, Vector2 boundaries)
    {
        var followCamera = _mainCamera.GetComponent<FollowCamera>();
        followCamera.CurrentLevelBoundaries = boundaries;
        followCamera.Target = target;
        followCamera.SetFollowPlayer(true);
    }
    
    private void MakeSpawnPoint()
    {
        _playerStart = new GameObject();
        _playerStart.transform.position = Vector2.zero;
    }

    private IEnumerator SpawnPlayer(bool delaySpawn = true)
    {
        _playerStart = GameObject.FindGameObjectWithTag("PlayerStart");

        if (!_playerStart)
        {
            Debug.LogWarning("[GameManager] No Player Spawn Point defined. Creating default at (0;0)");
            MakeSpawnPoint();
        }

        if (!player) Debug.LogWarning("[GameManager] Missing Player object reference.");

        if (!_playerController)
        {
            if (delaySpawn)
                yield return new WaitForSeconds(2);
            var spawnPointPosition = _playerStart.transform.position;
            _playerController = Instantiate(player, new Vector2(spawnPointPosition.x, spawnPointPosition.y),
                Quaternion.identity);
            _playerController.PlayerDead += OnPlayerDead;
            PlayerSpawned?.Invoke();

            // Cheap way to avoid sending a message internally to this same class. Meh.
            OnPlayerSpawned(_playerController);
        }
        else
        {
            Debug.LogWarning("Attempt to spawn player when one exists already.");
        }

        yield return null;
    }

    private void OnPlayerOutOfLives()
    {
        SceneManager.Instance.LoadLevel(3, levelTransition.GetComponent<Animator>());
        CurrentLevelIndex = 3;
    }
    
    private void OnPlayerDead()
    {
        CurrentPlayerLives--;

        _playerController = null;
        if (CurrentPlayerLives == 0)
        {
            OnPlayerOutOfLives();
            return;
        }
        StartCoroutine(SpawnPlayer());
    }

    public PlayerController GetPlayerController()
    {
        return _playerController;
    }
}