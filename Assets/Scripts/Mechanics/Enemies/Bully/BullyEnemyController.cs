using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyEnemyController : MovingObjectController
{
    public BullyAttackState attack;
    public BullyIdleState idle;
    public Bullet bullet;
    public float shootingInterval;
    public float shootingSpeed;

    protected BullyBaseState currentState;
    protected void Start()
    {
        idle = new BullyIdleState(this);
        attack = new BullyAttackState(this);

        currentState = attack;
        currentState.Enter();
    }

    protected override void FixedUpdate()
    {
        currentState.FixedUpdate();
        base.FixedUpdate();
    }

    protected void ChangeState(BullyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }
}
