using UnityEngine;

public class DoggoRandomMoveState : DoggoBaseState
{
    public DoggoRandomMoveState(DoggoController doggo) : base(doggo)
    {
    }
    private Vector2 _moveTo;
    private float _elapsedTime;
    
    public override void Enter()
    {
        _moveTo = (Vector2)doggo.transform.position + Random.insideUnitCircle.normalized * Random.Range(2,5);
        _elapsedTime = 0f;
    }

    public override void Update()
    {
        _elapsedTime += Time.fixedDeltaTime;
        if (Vector2.Distance(doggo.transform.position, _moveTo) <= 0.1f || _elapsedTime > doggo.maxRandomMoveTime)
        {
            doggo.ChangeState(doggo.idleState);
        }
        else
        {
            doggo.Velocity = (_moveTo - (Vector2) doggo.transform.position) * doggo.maxSpeed;
        }
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}