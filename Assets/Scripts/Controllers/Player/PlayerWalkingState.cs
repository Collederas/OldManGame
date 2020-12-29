using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalkingState : PlayerBaseState
{
    protected Vector2 inputAcceleration;

    public PlayerWalkingState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
    }

    public override void OnMove(InputValue value)
    {
        inputAcceleration = value.Get<Vector2>();
    }

    public override void OnFire()
    {
        player.ChangeState(player.boostState);
    }

    public override void Update()
    {
        player.Velocity = player.moveAction.ReadValue<Vector2>();
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}