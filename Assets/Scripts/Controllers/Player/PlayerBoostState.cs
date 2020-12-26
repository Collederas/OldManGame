using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBoostState : PlayerBaseState
{
    private float _elapsedTime;
    private int _entryLayer;

    public PlayerBoostState(PlayerController player) : base(player){}

    public override void Enter()
    {
        _elapsedTime = 0f;
        player.maxSpeed = player.maxBoostingSpeed;
        _entryLayer = player.gameObject.layer;
        player.gameObject.layer = LayerMask.NameToLayer("Invincible");
    }

    public override void OnMove(InputValue value)
    {
    }

    public override void OnFire()
    {
    }
    public override void Update()
    {

    }
    public override void FixedUpdate()
    {
        var boostDistance = player.boostDistance;
        var boostDuration = player.boostDuration;

        if (_elapsedTime <= boostDuration) {
            player.Velocity = player.Velocity.normalized * (boostDistance/boostDuration);
            _elapsedTime += Time.fixedDeltaTime;
        }
        else {
            player.ChangeState(player.walkingState);
        }
    }

    public override void Exit()
    {
        player.Velocity = Vector2.zero;
        player.maxSpeed = player.maxWalkingSpeed;
        player.gameObject.layer = _entryLayer;
    }
}
