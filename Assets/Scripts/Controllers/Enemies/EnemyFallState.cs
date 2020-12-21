using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFallState : EnemyBaseState
{

    protected EnemyStateMachineController enemy;

    public EnemyFallState(EnemyStateMachineController enemy) 
    {
        this.enemy = enemy;
    }
    float elapsedTime;

    public override void Enter()
    {
        elapsedTime = 0f;
        enemy.Velocity = Vector2.zero;
    }

    public override void Update()
    {

    }

    public override void FixedUpdate()
    {
        if (elapsedTime < 2f)
        {
            enemy.transform.localScale = Vector2.Lerp(enemy.transform.localScale, Vector2.zero, enemy.fallingSpeed * Time.fixedDeltaTime);
            enemy.transform.position = Vector2.Lerp(enemy.transform.position, enemy.FallTargetPosition, enemy.fallingSpeed * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
        } else {
            enemy.Die();
        }
    }

    public override void Exit()
    {

    }
}
