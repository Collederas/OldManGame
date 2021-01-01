using UnityEngine;

public class DoggoRandomMoveState : DoggoBaseState
{
    public DoggoRandomMoveState(DoggoController doggo) : base(doggo)
    {
    }
    public Vector2 moveTo;
    private float _elapsedTime;

    public void CalculateNewDestination()
    {
        moveTo = (Vector2)doggo.transform.position + Random.insideUnitCircle.normalized * Random.Range(2,5);
        _elapsedTime = 0f;
    }
    
    public override void Enter()
    {
        CalculateNewDestination();
    }

    public override void Update()
    {
        _elapsedTime += Time.fixedDeltaTime;
        if (Vector2.Distance(doggo.transform.position, moveTo) <= 0.1f || _elapsedTime > doggo.maxRandomMoveTime)
        {
            doggo.ChangeState(doggo.idleState);
        }
        else
        {
            doggo.Velocity = (moveTo - (Vector2) doggo.transform.position) * doggo.maxSpeed;
        }
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}