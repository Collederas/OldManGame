using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    public HealthBar healthBar;

    public void Awake()
    {
        if (healthBar == null)
            healthBar = GetComponentInChildren<HealthBar>();
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthBar.SetHealth(currentHealth);
    }
}