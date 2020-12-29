using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        player.Velocity = Vector2.zero;
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }

    public override void OnMove(InputValue value)
    {
    }

    public override void OnFire()
    {
    }
}