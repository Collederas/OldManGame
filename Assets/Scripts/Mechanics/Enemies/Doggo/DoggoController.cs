using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoggoController : EnemyStateMachineController
{
    public GameObject target;
    public DoggoIdleState idleState;
    public DoggoChaseState chaseState;

    protected override void Start()
    {
        gameManager.PlayerSpawned += OnPlayerSpawned;
        idleState = new DoggoIdleState(this);
        chaseState = new DoggoChaseState(this);

        currentState = idleState;
        base.Start();
    }

    protected void OnPlayerSpawned()
    {
        ChangeState(chaseState);
    }
}
