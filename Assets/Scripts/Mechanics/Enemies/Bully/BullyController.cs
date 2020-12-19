public class BullyController : EnemyStateMachineController, IKillable
{
    public BullyAttackState attackState;
    public BullyIdleState idleState;
    public Bullet bullet;
    public float shootingInterval = 2f;
    public float shootingSpeed = 8f;

    protected override void Awake()
    {
        base.Awake();
        gameManager.PlayerSpawned += OnPlayerSpawned;
    }

    protected override void Start()
    {
        idleState = new BullyIdleState(this);
        attackState = new BullyAttackState(this);

        currentState = idleState;
        base.Start();
    }

    private void OnPlayerSpawned()
    {
        ChangeState(attackState);
    }
}
