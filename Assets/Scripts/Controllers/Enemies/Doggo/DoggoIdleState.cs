using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoggoIdleState : DoggoBaseState
{
    public DoggoIdleState(DoggoController doggo) : base(doggo)
    {
    }
    
    private float _elapsedTime;
    private float _stillTime;
    
    public override void Enter()
    {
        _stillTime = Random.Range(1,4);
        _elapsedTime = 0f;
        doggo.Velocity = Vector2.zero;
    }

    public override void Update()
    {
        if (doggo.frozen) return;
        _elapsedTime += Time.fixedDeltaTime;
        if(_elapsedTime > _stillTime)
            doggo.ChangeState(doggo.randomMoveState);
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}