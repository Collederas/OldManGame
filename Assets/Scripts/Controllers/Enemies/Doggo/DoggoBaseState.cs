public abstract class DoggoBaseState : EnemyBaseState
{
    protected DoggoController doggo;

    public DoggoBaseState(DoggoController doggo)
    {
        this.doggo = doggo;
    }
}