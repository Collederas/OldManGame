public class BullyController : EnemyStateMachineController, IKillable
{
    public Bullet bullet;
    public float shootingInterval = 2f;
    public float shootingSpeed = 8f;
    public BullyAttackState attackState;
    public BullyIdleState idleState;

    private void Awake()
    {
        GameManager.Instance.PlayerSpawned += OnPlayerSpawned;
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

    private void OnFOVTagLost()
    {
        ChangeState(idleState);
    }
}