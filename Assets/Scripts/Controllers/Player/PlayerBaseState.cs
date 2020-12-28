using UnityEngine.InputSystem;

public abstract class PlayerBaseState
{
    protected PlayerController player;

    public PlayerBaseState(PlayerController player)
    {
        this.player = player;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
    public abstract void OnMove(InputValue value);
    public abstract void OnFire();
}