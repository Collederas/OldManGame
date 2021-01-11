using Components;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MovingObjectController
{
    private PlayerInput _playerInput;
    public InputAction moveAction;

    [Range(0.0f, 5.0f)] public float boostDuration = 1f;

    [Range(0.0f, 10.0f)] public float boostDistance = 2f;

    public float maxBoostingSpeed = 15f;
    public float maxWalkingSpeed = 2f;
    public float fallingSpeed = 2f;

    private Vector2 _impulseAcceleration;
    private Vector2 _inputAcceleration;
    private DamageComponent _damageComponent;
    
    public PlayerBoostState boostState;
    public PlayerBaseState currentState;
    public PlayerFallState fallState;
    public PlayerIdleState idleState;
    public PlayerWalkingState walkingState;

    public Vector2 FallTargetPosition { get; set; }
    
    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _damageComponent = GetComponent<DamageComponent>();
        
        /* Instantiated this way to allow more precise
        control over movement via polling. */
        moveAction = _playerInput.actions["move"];
        
        maxSpeed = maxWalkingSpeed;
        walkingState = new PlayerWalkingState(this);
        boostState = new PlayerBoostState(this);
        fallState = new PlayerFallState(this);
        idleState = new PlayerIdleState(this);
        currentState = walkingState;
    }

    protected override void Update()
    {
        currentState.Update();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        currentState.FixedUpdate();
        base.FixedUpdate();
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

    public void OnHPBelowZero()
    {
        Die();
    }

    public void OnFire()
    {
        currentState.OnFire();
    }
    
    public override void Fall(Vector2 fallTargetPosition)
    {
        if (currentState is PlayerBoostState) return;
        ChangeState(fallState);
        FallTargetPosition = fallTargetPosition;
    }

    public void Die()
    {
        GameManager.Instance.OnPlayerDead();
    }
}