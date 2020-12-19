using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public event Action PlayerSpawned;
    public InputAction spawnPlayer;
    public PlayerController player;
    public GameObject playerSpawnPoint;
    public LevelMaster levelMaster;

    private PlayerController _playerInstance;
    
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
        if (playerSpawnPoint == null)
        {
            Debug.LogWarning("No Player Spawn Point defined. Creating default at (0;0)");
            MakeSpawnPoint();
        }
        
        if(_playerInstance == null)
            SpawnPlayer();
    }
    
    private void OnSpawnPlayer(InputAction.CallbackContext context)
    {
        SpawnPlayer();
    }

    private void MakeSpawnPoint()
    {
        playerSpawnPoint = new GameObject();
        playerSpawnPoint.transform.position = Vector2.zero;
    }
    private void SpawnPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("GameManager script is missing a Player object reference.");
            return;
        }

        if (_playerInstance == null )
        {
            var spawnPointPosition = playerSpawnPoint.transform.position;
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
