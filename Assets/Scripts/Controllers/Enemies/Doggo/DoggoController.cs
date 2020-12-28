using UnityEngine;

public class DoggoController : EnemyStateMachineController
{
    public GameObject target;
    public DoggoChaseState chaseState;
    public DoggoIdleState idleState;

    private void Awake()
    {
        GameManager.Instance.PlayerSpawned += OnPlayerSpawned;
        idleState = new DoggoIdleState(this);
        chaseState = new DoggoChaseState(this);

        currentState = idleState;
    }

    private void OnPlayerSpawned()
    {
        ChangeState(chaseState);
    }
}