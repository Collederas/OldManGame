using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyEnemyController : MovingObjectController
{
    public BullyAttackState attack;
    public BullyIdleState idle;
    public BullyFallState fall;
    public Bullet bullet;
    public float shootingInterval = 2f;
    public float shootingSpeed = 8f;
    public float fallingSpeed = 2f;

    protected BullyBaseState currentState;
    protected void Start()
    {
        idle = new BullyIdleState(this);
        attack = new BullyAttackState(this);
        fall = new BullyFallState(this);

        currentState = attack;
        currentState.Enter();
    }

    public override void Fall()
    {
        ChangeState(fall);
    }
    protected override void FixedUpdate()
    {
        currentState.FixedUpdate();
        base.FixedUpdate();
    }

    public void ChangeState(BullyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }
}
