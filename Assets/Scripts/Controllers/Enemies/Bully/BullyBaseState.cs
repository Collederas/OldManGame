public abstract class BullyBaseState : EnemyBaseState
{
    protected BullyController bully;

    public BullyBaseState(BullyController enemy)
    {
        bully = enemy;
    }
}