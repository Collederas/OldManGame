using UnityEngine;

public class DoggoController : EnemyStateMachineController
{
    public GameObject target;
    public DoggoChaseState chaseState;
    public DoggoIdleState idleState;
    public DoggoAlertState alertState;
    public DoggoRandomMoveState randomMoveState;
    
    // Max time Doggo can spend in randomMoveState
    public float maxRandomMoveTime = 3f;
    
    private void Awake()
    {
        idleState = new DoggoIdleState(this);
        chaseState = new DoggoChaseState(this);
        alertState = new DoggoAlertState(this);
        randomMoveState = new DoggoRandomMoveState(this);

        currentState = idleState;
    }

    private void OnFOVTagDetected()
    {
        ChangeState(alertState);
    }
    
    private void OnFOVTagLost()
    {
        ChangeState(idleState);
    }
}