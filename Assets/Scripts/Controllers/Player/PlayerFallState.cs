using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFallState : PlayerBaseState
{
    float elapsedTime;
    public PlayerFallState(PlayerController player) : base(player) { }

    public override void Enter()
    {
        elapsedTime = 0f;
        player.Velocity = Vector2.zero;
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
        if (elapsedTime < 0.8f)
        {
            player.transform.localScale = Vector2.Lerp(player.transform.localScale, Vector2.zero, player.fallingSpeed * Time.fixedDeltaTime);
            player.transform.position = Vector2.Lerp(player.transform.position, player.FallTargetPosition, player.fallingSpeed * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
        } else {
            player.CurrentHealth = 0;
        }
    }

    public override void Exit()
    {
    }
}
