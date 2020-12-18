using UnityEngine;

public class DoggoChaseState : DoggoBaseState
{
    public DoggoChaseState(DoggoController doggo): base(doggo) {}
    GameObject target;

    public override void Enter()
    {
        target = doggo.gameManager.GetPlayer().gameObject;
        if (target == null)
            doggo.ChangeState(doggo.idleState);
    }

    public override void Update()
    {

    }
    
    public override void FixedUpdate()
    {
        doggo.Velocity = ((Vector2) target.transform.position - (Vector2) doggo.transform.position).normalized * doggo.maxSpeed;
    }

    public override void Exit()
    {

    }
}
