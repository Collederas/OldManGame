using UnityEngine;

public class EnemyStateMachineController : MovingObjectController
{
    public float fallingSpeed = 2f;
    protected EnemyBaseState currentState;
    protected EnemyFallState fallState;
    public Vector2 FallTargetPosition { get; set; }

    protected virtual void Start()
    {
        fallState = new EnemyFallState(this);
        currentState.Enter();
    }

    protected override void Update()
    {
        currentState.Update();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        currentState.FixedUpdate();
        base.FixedUpdate();
    }

    public void ChangeState(EnemyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    public override void Fall(Vector2 fallTargetPosition)
    {
        FallTargetPosition = fallTargetPosition;
        ChangeState(fallState);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}