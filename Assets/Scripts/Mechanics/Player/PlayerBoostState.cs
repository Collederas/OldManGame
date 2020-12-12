using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoostState : PlayerBaseState
{
    float elapsedTime;
    Collider2D playerCollider;

    public PlayerBoostState(PlayerController player) : base(player){}

    public override void Enter()
    {
        elapsedTime = 0f;
        player.maxSpeed = player.maxBoostingSpeed;
        playerCollider = player.GetComponent<Collider2D>();
        // playerCollider.enabled = false;
    }

    public override void OnMove(InputValue value)
    {
    }

    public override void OnFire()
    {
    }
    public override void Update()
    {
        var boostDistance = player.boostDistance;
        var boostDuration = player.boostDuration;

        if (elapsedTime <= boostDuration) {
            player.Velocity = player.Velocity.normalized * (boostDistance/boostDuration);
            elapsedTime += Time.fixedDeltaTime;
        }
        else {
            player.ChangeState(player.walkingState);
        }
    }
    public override void FixedUpdate()
    {

    }

    public override void Exit()
    {
        player.Velocity = Vector2.zero;
        player.maxSpeed = player.maxWalkingSpeed;
        // playerCollider.enabled = true;
    }
}
