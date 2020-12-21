using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BullyAttackState : BullyBaseState
{
    public bool bCanShoot;
    private float elapsedTime;
    GameManager gameManager;
    
    GameObject target;

    public BullyAttackState(BullyController enemy) : base(enemy) {}


    public override void Enter()
    {
        elapsedTime = 0f;
        target = bully.gameManager.GetPlayer().gameObject;
        if (target == null)
            bully.ChangeState(bully.idleState);
    }

    public override void Update()
    {

    }
    public override void FixedUpdate()
    {   
        if (target) 
        {
        if (elapsedTime > bully.shootingInterval)
        {
            var obj = GameObject.Instantiate(
                bully.bullet,
                new Vector3(bully.transform.position.x, bully.transform.position.y, 0),
                Quaternion.identity
            );
            obj.Target = target.transform.position;
            obj.Speed = bully.shootingSpeed;
            elapsedTime = 0f;
        }
        else
        {
            elapsedTime += Time.fixedDeltaTime;
        }
        } else 
        {
            bully.ChangeState(bully.idleState);
        }
    }

    public override void Exit()
    {

    }
}
