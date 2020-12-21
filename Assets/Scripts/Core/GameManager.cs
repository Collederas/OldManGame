using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public event Action PlayerSpawned;
    public InputAction spawnPlayer;
    public PlayerController player;
    public LevelMaster levelMaster;

    private PlayerController _playerInstance;
    private GameObject _playerStart;

    private void Awake()
    {        
        DontDestroyOnLoad(gameObject);
        levelMaster.LevelLoaded += InitializeLevel;
    }

    private void Start()
    {
        SpawnPlayer();

        spawnPlayer.performed += OnSpawnPlayer;
    }

    private void OnEnable()
    {
        spawnPlayer.Enable();
    }

    private void InitializeLevel(Level level)
    {
        if(_playerInstance == null)
            SpawnPlayer();
    }
    
    private void OnSpawnPlayer(InputAction.CallbackContext context)
    {
        SpawnPlayer();
    }

    private void MakeSpawnPoint()
    {
        _playerStart = new GameObject();
        _playerStart.transform.position = Vector2.zero;
    }
    private void SpawnPlayer()
    {
        _playerStart = GameObject.FindGameObjectWithTag("PlayerStart");

        if (_playerStart == null)
        {
            Debug.LogWarning("No Player Spawn Point defined. Creating default at (0;0)");
            MakeSpawnPoint();
        }
        
        if (player == null)
        {
            Debug.LogWarning("GameManager script is missing a Player object reference.");
            return;
        }

        if (_playerInstance == null )
        {
            var spawnPointPosition = _playerStart.transform.position;
            _playerInstance = Instantiate(player, new Vector2(spawnPointPosition.x, spawnPointPosition.y), Quaternion.identity);
            PlayerSpawned?.Invoke();
        }
        else
        {
            Debug.LogWarning("Attempt to spawn player when one exists already.");
        }
    }

    public PlayerController GetPlayer()
    {
        return _playerInstance;
    }

    public LevelMaster GetLevelMaster()
    {
        return levelMaster;
    }
}
