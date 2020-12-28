using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthBar))]
public class PlayerController : MovingObjectController, IDamageable, IKillable
{
    public event Action PlayerDead;
    public PlayerBaseState currentState;
    public PlayerIdleState idleState;
    public PlayerWalkingState walkingState;
    public PlayerBoostState boostState;
    public PlayerFallState fallState;
    
    public Vector2 FallTargetPosition { get; set; }

    public int lives = 5;
    public int maxHealth = 4;

    private int _currentHealth;
    public int CurrentHealth 
    { 
        get => _currentHealth;
        set 
        {
            _currentHealth = value;
            BroadcastMessage("UpdateHealthBar", value);
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
        idleState = new PlayerIdleState(this);
        currentState = walkingState;
    }

    public void DeactivateInput()
    {
        GetComponent<PlayerInput>().DeactivateInput();
        ChangeState(idleState);
    }
    
    public void ActivateInput()
    {
        GetComponent<PlayerInput>().ActivateInput();
        ChangeState(walkingState);
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
        lives--;
        if (lives == 0)
            Debug.Log("Send message lives = 0");
        else
            PlayerDead?.Invoke();
        Destroy(gameObject);
    }
}
