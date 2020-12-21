using UnityEngine;

public class DoggoController : EnemyStateMachineController
{
    public GameObject target;
    public DoggoIdleState idleState;
    public DoggoChaseState chaseState;

    protected override void Awake()
    {
        base.Awake();

        gameManager.PlayerSpawned += OnPlayerSpawned;
        idleState = new DoggoIdleState(this);
        chaseState = new DoggoChaseState(this);

        currentState = idleState;
    }
    protected void OnPlayerSpawned()
    {
        ChangeState(chaseState);
    }
}
