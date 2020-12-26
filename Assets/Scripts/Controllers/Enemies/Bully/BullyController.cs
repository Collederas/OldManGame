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
        idleState = new BullyIdleState(this);
        attackState = new BullyAttackState(this);
        currentState = idleState;
    } 
    private void OnPlayerSpawned()
    {
        if (currentState == null)
            currentState = new BullyIdleState(this);
    }

    private void OnFOVTagDetected()
    {
        ChangeState(attackState);
    }
}
