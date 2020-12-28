using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Pregame,
        Running
    }

    private static Camera _mainCamera;

    public PlayerController player;
    public LevelManager levelManager;

    public GameObject levelTransition;

    private PlayerController _playerController;
    private GameObject _playerStart;

    public GameState CurrentGameState { get; private set; } = GameState.Pregame;

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = FindObjectOfType<Camera>();

        if (levelManager) return;
        Debug.LogError("[GameManager] No LevelManager set. Quit the application and set the reference.");
        Debug.Break();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        levelManager.LevelLoaded += InitializeLevel;
        levelManager.LoadLevel(0, levelTransition.GetComponent<Animator>());
    }

    public event Action PlayerSpawned;

    public void StartGame()
    {
        UpdateState(GameState.Running);
        levelManager.UnloadCurrentLevel();
        levelManager.LoadLevel(1, levelTransition.GetComponent<Animator>());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateState(GameState newState)
    {
        CurrentGameState = newState;
        switch (newState)
        {
            case GameState.Pregame:
                break;
            case GameState.Running:
                break;
        }
    }

    private void InitializeLevel(Level level)
    {
        if (level.levelType != Level.LevelType.Gameplay) return;
        if (_playerController) return;
        StartCoroutine(SpawnPlayer(false));
    }

    private void OnPlayerSpawned(PlayerController playerController)
    {
        SetupFollowCamera(playerController.gameObject, levelManager.GetCurrentLevel().levelSize);
    }

    private void SetupFollowCamera(GameObject target, Vector2 boundaries)
    {
        var followCamera = _mainCamera.GetComponent<FollowCamera>();
        followCamera.CurrentLevelBoundaries = boundaries;
        followCamera.Target = target;
        followCamera.SetFollowPlayer(true);
    }

    private void OnPlayerDead()
    {
        _playerController = null;
        StartCoroutine(SpawnPlayer());
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

    public PlayerController GetPlayerController()
    {
        return _playerController;
    }
}