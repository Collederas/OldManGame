using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public event Action PlayerSpawned;
    public InputAction spawnPlayer;
    public PlayerController player;
    private PlayerController PlayerInstance;

    public Camera mainCamera;

    public GameObject playerSpawnPoint;
    public Level[] levels;
    

    void Start()
    {
        spawnPlayer.performed += OnSpawnPlayer;
        SpawnPlayer();
    }

    void OnEnable()
    {
        spawnPlayer.Enable();
    }

    void OnSpawnPlayer(InputAction.CallbackContext context)
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("GameManager script is missing a Player object reference. Please define it in the Inspector.");
            return;
        }
        PlayerInstance = Instantiate(player, new Vector2(playerSpawnPoint.transform.position.x, playerSpawnPoint.transform.position.y), Quaternion.identity);
        if (PlayerSpawned != null)
            PlayerSpawned();
    }

    public PlayerController GetPlayer()
    {
        if (PlayerInstance)
            return PlayerInstance;

        Debug.LogWarning("No player!");
        return PlayerInstance;
    }

    public Level GetCurrentLevel()
    {
        return levels[0];
    }
}
