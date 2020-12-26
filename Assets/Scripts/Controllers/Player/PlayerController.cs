using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthBar))]
public class PlayerController : MovingObjectController, IDamageable, IKillable
{   
    public Action updateHealth;
    public PlayerBaseState currentState;
    public PlayerWalkingState walkingState;
    public PlayerBoostState boostState;
    public PlayerFallState fallState;
    
    public Vector2 FallTargetPosition { get; set; }

    public int maxHealth = 4;

    private int _currentHealth;
    public int CurrentHealth 
    { 
        get => _currentHealth;
        set 
        {
            _currentHealth = value;
            updateHealth?.Invoke();
        }
    }


    [Range(0.0f, 5.0f)]
    public float boostDuration = 1f;
    [Range(0.0f, 10.0f)]
    public float boostDistance = 2f;
    public float maxBoostingSpeed = 15f;
    public float maxWalkingSpeed = 2f;
    public float fallingSpeed = 2f;

    private Vector2 _inputAcceleration;
    private Vector2 _impulseAcceleration;

    public void Start()
    {   
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
        currentState?.OnMove(value);
    }

    public void OnFire()
    {
        currentState.OnFire();
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
    }

    public override void Fall(Vector2 fallTargetPosition)
    {
        if (currentState is PlayerBoostState) return;
        ChangeState(fallState);
        FallTargetPosition = fallTargetPosition;
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
