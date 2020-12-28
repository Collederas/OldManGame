using UnityEngine;

public class BullyAttackState : BullyBaseState
{
    private float _elapsedTime;
    private GameObject _target;

    public BullyAttackState(BullyController enemy) : base(enemy)
    {
    }


    public override void Enter()
    {
        _elapsedTime = 0f;
        _target = GameManager.Instance.GetPlayerController().gameObject;
        if (!_target)
            bully.ChangeState(bully.idleState);
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        if (_target)
        {
            if (_elapsedTime > bully.shootingInterval)
            {
                var obj = Object.Instantiate(
                    bully.bullet,
                    new Vector3(bully.transform.position.x, bully.transform.position.y, 0),
                    Quaternion.identity
                );
                obj.Target = _target.transform.position;
                obj.Speed = bully.shootingSpeed;
                _elapsedTime = 0f;
            }
            else
            {
                _elapsedTime += Time.fixedDeltaTime;
            }
        }
        else
        {
            bully.ChangeState(bully.idleState);
        }
    }

    public override void Exit()
    {
    }
}