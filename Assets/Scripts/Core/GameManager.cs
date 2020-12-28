using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public event Action PlayerSpawned;
    public PlayerController player;
    public LevelMaster levelMaster;

    private PlayerController _playerInstance;
    private Camera _mainCamera;
    private GameObject _playerStart;

    private void Awake()
    {        
        DontDestroyOnLoad(gameObject);
        _mainCamera = FindObjectOfType<Camera>();
        levelMaster.LevelLoaded += InitializeLevel;
    }

    private void Start()
    {
        StartCoroutine(SpawnPlayer(false));
    }
    
    private void InitializeLevel(Level level)
    {
        if (_playerInstance != null) return;
        StartCoroutine(SpawnPlayer());
    }

    private void OnPlayerDead()
    {
        _playerInstance = null;
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
            Debug.LogWarning("No Player Spawn Point defined. Creating default at (0;0)");
            MakeSpawnPoint();
        }
        
        if (!player)
        {
            Debug.LogWarning("GameManager script is missing a Player object reference.");
        }

        if (!_playerInstance)
        {
            if (delaySpawn)
                yield return new WaitForSeconds(2);
            var spawnPointPosition = _playerStart.transform.position;
            _playerInstance = Instantiate(player, new Vector2(spawnPointPosition.x, spawnPointPosition.y), Quaternion.identity);
            _playerInstance.PlayerDead += OnPlayerDead;
            PlayerSpawned?.Invoke();
        }
        else
        {
            Debug.LogWarning("Attempt to spawn player when one exists already.");
        }

        yield return null;
    }

    public PlayerController GetPlayer()
    {
        return _playerInstance;
    }
    
    public Camera GetMainCamera()
    {
        return _mainCamera;
    }

    public LevelMaster GetLevelMaster()
    {
        return levelMaster;
    }
}
