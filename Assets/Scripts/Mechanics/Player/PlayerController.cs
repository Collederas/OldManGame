using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovingObjectController
{   
    public PlayerBaseState currentState;
    public PlayerWalkingState walkingState;
    public PlayerBoostState boostState;


    [Range(0.0f, 5.0f)]
    public float boostDuration = 1f;
    [Range(0.0f, 10.0f)]
    public float boostDistance = 5f;
    public float maxBoostingSpeed = 15f;
    public float maxWalkingSpeed = 2f;

    private Vector2 inputAcceleration;
    private Vector2 impulseAcceleration;

    public void Start()
    {
        maxSpeed = maxWalkingSpeed;
        walkingState = new PlayerWalkingState(this);
        boostState = new PlayerBoostState(this);
        currentState = walkingState;
    }
    
    public void ChangeState(PlayerBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    public void OnMove(InputValue value)
    {
        currentState.OnMove(value);
    }

    public void OnFire()
    {
        currentState.OnFire();
    }

    protected override void Update()
    {
        currentState.Update();
        base.Update();
    }
}
