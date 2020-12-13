using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(HealthBar))]
public class PlayerController : MovingObjectController, IDamageable, IKillable
{   
    public PlayerBaseState currentState;
    public PlayerWalkingState walkingState;
    public PlayerBoostState boostState;
    public PlayerFallState fallState;


    public int maxHealth = 4;
    public HealthBar healthBar;

    protected int currentHealth;


    [Range(0.0f, 5.0f)]
    public float boostDuration = 1f;
    [Range(0.0f, 10.0f)]
    public float boostDistance = 5f;
    public float maxBoostingSpeed = 15f;
    public float maxWalkingSpeed = 2f;
    public float fallingSpeed = 2f;

    [HideInInspector]
    public int defaultLayer;

    private Vector2 inputAcceleration;
    private Vector2 impulseAcceleration;
    private bool bUpdateHealthBar;

    public void Start()
    {   
        defaultLayer = LayerMask.NameToLayer("Default");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

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

    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
        bUpdateHealthBar = false;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        bUpdateHealthBar = true;
    }

    public override void Fall()
    {
        if (!(currentState is PlayerBoostState))
            ChangeState(fallState);
    }

    protected override void Update()
    {
        currentState.Update();
        if (bUpdateHealthBar)
            UpdateHealthBar();
        if(currentHealth <= 0)
            Die();
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
