using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public event Action PlayerSpawned;
    public InputAction spawnPlayer;
    public PlayerController player;
    public PlayerController PlayerInstance { get; set; }

    public HealthBar healthBar;
    public GameObject spawnPoint;


    void Awake()
    {
        spawnPlayer.performed += OnPlayerSpawn;
    }

    void Start()
    {
        SpawnPlayer();
    }

    void OnEnable()
    {
        spawnPlayer.Enable();
    }

    void OnPlayerSpawn(InputAction.CallbackContext context)
    {
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        PlayerInstance = Instantiate(player, new Vector2(spawnPoint.transform.position.x, spawnPoint.transform.position.y), Quaternion.identity);
        if (PlayerSpawned != null)
            PlayerSpawned();
    }
}
