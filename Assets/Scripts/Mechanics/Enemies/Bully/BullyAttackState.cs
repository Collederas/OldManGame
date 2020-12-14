using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BullyAttackState : BullyBaseState
{
    public bool bCanShoot;
    private float elapsedTime;
    GameManager gameManager;

    public BullyAttackState(BullyEnemyController enemy) : base(enemy) { }

    public override void Enter()
    {
        elapsedTime = 0f;
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public override void Update()
    {

    }
    public override void FixedUpdate()
    {
        if (gameManager.PlayerInstance)
        {
            if (elapsedTime > enemy.shootingInterval)
            {
                var obj = GameObject.Instantiate(
                    enemy.bullet,
                    new Vector3(enemy.transform.position.x, enemy.transform.position.y, 0),
                    Quaternion.identity
                );
                obj.Target = gameManager.PlayerInstance.transform.position;
                obj.Speed = enemy.shootingSpeed;
                elapsedTime = 0f;
            }
            else
            {
                elapsedTime += Time.fixedDeltaTime;
            }
        } 
        else
        {
            enemy.ChangeState(enemy.idle);
        } 
    }

    public override void Exit()
    {

    }
}
