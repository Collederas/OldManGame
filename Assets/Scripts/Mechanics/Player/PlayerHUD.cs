using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthBar))]
public class PlayerHUD : MonoBehaviour
{
    public HealthBar healthBar;
    GameManager gameManager;


    public void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.PlayerSpawned += OnPlayerSpawned;
    }

    void OnPlayerSpawned()
    {
        if (gameManager)
        {
            gameManager.GetPlayer().UpdateHealth += UpdateHealthBar;
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.SetHealth(gameManager.GetPlayer().CurrentHealth);
    }
}
