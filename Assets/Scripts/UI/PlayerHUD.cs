public class PlayerHUD : Singleton<PlayerHUD>
{
    private LivesCounter _livesCounter;
    private BoostCounter _boostCounter;
    private bool _boostCounterActive;

    protected override void Awake()
    {
        base.Awake();
        _livesCounter = GetComponentInChildren<LivesCounter>();
        _boostCounter = GetComponentInChildren<BoostCounter>();
    }

    public void Start()
    {
        GameManager.Instance.GameStateChanged += OnGameStateChanged;
        GameManager.Instance.LivesUpdated += OnUpdateLivesCount;
        GameManager.Instance.BoostCounterUpdated += OnUpdateBoostCount;
        gameObject.SetActive(false);
    }
    
    private void OnGameStateChanged(GameManager.GameState previousState, GameManager.GameState currentState)
    {
        if (currentState == GameManager.GameState.Running)
            gameObject.SetActive(true);
    }
    
    private void OnUpdateLivesCount(int currentLives)
    {
        _livesCounter.SetCount(currentLives);
    }
    
    private void OnUpdateBoostCount(int currentBoostAmount)
    {
        if (!_boostCounterActive)
        {
            _boostCounterActive = true;
            _boostCounter.Appear();
        }
        _boostCounter.SetCount(currentBoostAmount);
    }
}