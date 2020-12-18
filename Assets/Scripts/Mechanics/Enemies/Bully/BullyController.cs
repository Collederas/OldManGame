using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyController : EnemyStateMachineController, IKillable
{
    public BullyAttackState attackState;
    public BullyIdleState idleState;
    public Bullet bullet;
    public float shootingInterval = 2f;
    public float shootingSpeed = 8f;


    protected override void Start()
    {
        gameManager.PlayerSpawned += OnPlayerSpawned;
        idleState = new BullyIdleState(this);
        attackState = new BullyAttackState(this);

        currentState = idleState;
        base.Start();
    }

    protected void OnPlayerSpawned()
    {
        ChangeState(attackState);
    }
}
