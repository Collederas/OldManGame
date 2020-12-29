public class PlayerHUD : Singleton<PlayerHUD>
{
    private HealthBar _healthBar;
    private LivesCounter _livesCounter;

    protected override void Awake()
    {
        base.Awake();
        _healthBar = GetComponentInChildren<HealthBar>();
        _livesCounter = GetComponentInChildren<LivesCounter>();
    }

    public void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
        GameManager.Instance.PlayerSpawned += OnPlayerSpawned;
        GameManager.Instance.LivesUpdated += OnUpdateLivesCount;
    }

    private void OnPlayerSpawned()
    {
        GameManager.Instance.GetPlayerController().UpdateHealthBar += OnUpdateHealthBar;
    }
    
    private void OnGameStateChanged(GameManager.GameState previousState, GameManager.GameState currentState)
    {
        if (currentState == GameManager.GameState.Running)
            gameObject.SetActive(true);
    }

    private void OnUpdateHealthBar(int currentHealth)
    {
        _healthBar.SetHealth(currentHealth);
    }
    
    private void OnUpdateLivesCount(int currentLives)
    {
        _livesCounter.SetCount(currentLives);
    }
}