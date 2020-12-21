using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachineController : MovingObjectController
{
    public Vector2 FallTargetPosition { get; set; }

    protected EnemyBaseState currentState;
    protected EnemyFallState fallState;
    public float fallingSpeed = 2f;
    public GameManager gameManager;

    protected virtual void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Start()
    {
        fallState = new EnemyFallState(this);
        currentState.Enter();
    }

    public void ChangeState(EnemyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
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
