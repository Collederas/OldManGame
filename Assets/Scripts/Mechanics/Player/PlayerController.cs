using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthBar))]
public class PlayerController : MovingObjectController, IDamageable, IKillable
{   
    public Action UpdateHealth;
    public PlayerBaseState currentState;
    public PlayerWalkingState walkingState;
    public PlayerBoostState boostState;
    public PlayerFallState fallState;

    public int maxHealth = 4;

    private int currentHealth;
    public int CurrentHealth 
    { 
        get { return currentHealth; } 
        set 
        {
            currentHealth = value;
            if (UpdateHealth != null)
            {   
                UpdateHealth();
            }
        }
    }


    [Range(0.0f, 5.0f)]
    public float boostDuration = 1f;
    [Range(0.0f, 10.0f)]
    public float boostDistance = 2f;
    public float maxBoostingSpeed = 15f;
    public float maxWalkingSpeed = 2f;
    public float fallingSpeed = 2f;

    [HideInInspector]
    public int defaultLayer;

    private Vector2 inputAcceleration;
    private Vector2 impulseAcceleration;

    public void Start()
    {   
        defaultLayer = LayerMask.NameToLayer("Default");
        CurrentHealth = maxHealth;

        maxSpeed = maxWalkingSpeed;
        walkingState = new PlayerWalkingState(this);
        boostState = new PlayerBoostState(this);
        fallState = new PlayerFallState(this);
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

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
    }

    public override void Fall()
    {
        if (!(currentState is PlayerBoostState))
            ChangeState(fallState);
    }

    protected override void Update()
    {
        currentState.Update();
        if(CurrentHealth <= 0)
        {
            Die();
        }
        base.Update();
    }

    protected override void FixedUpdate()
    {
        currentState.FixedUpdate();
        base.FixedUpdate();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
