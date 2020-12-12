using UnityEngine;

public abstract class BullyBaseState
{
    protected BullyEnemyController enemy;

    public BullyBaseState(BullyEnemyController enemy)
    {
        this.enemy = enemy;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void Exit();
}
