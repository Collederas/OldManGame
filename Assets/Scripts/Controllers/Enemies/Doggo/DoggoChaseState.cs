using UnityEngine;

public class DoggoChaseState : DoggoBaseState
{
    private GameObject _target;

    public DoggoChaseState(DoggoController doggo) : base(doggo)
    {
    }

    public override void Enter()
    {
        _target = GameManager.Instance.GetPlayerController().gameObject;
        if (!_target)
            doggo.ChangeState(doggo.idleState);
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        doggo.Velocity = ((Vector2) _target.transform.position - (Vector2) doggo.transform.position).normalized *
                         doggo.maxSpeed;
    }

    public override void Exit()
    {
    }
}