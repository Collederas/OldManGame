using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public HealthBar healthBar;
    private GameManager _gameManager;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.PlayerSpawned += OnPlayerSpawned;

        if (healthBar == null)
            healthBar = FindObjectOfType<HealthBar>();
    }

    private void OnPlayerSpawned()
    {
        if (_gameManager)
        {
            _gameManager.GetPlayer().updateHealth += UpdateHealthBar;
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.SetHealth(_gameManager.GetPlayer().CurrentHealth);
    }
}
