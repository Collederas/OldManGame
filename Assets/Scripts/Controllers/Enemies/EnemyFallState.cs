﻿using System.Collections;
using UnityEngine;

public class EnemyFallState : EnemyBaseState
{
    private float elapsedTime;

    protected EnemyStateMachineController enemy;

    public EnemyFallState(EnemyStateMachineController enemy)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        elapsedTime = 0f;
        var fov = enemy.GetComponentInChildren<EnemyFOV>();
        if (fov)
            fov.gameObject.SetActive(false);
        enemy.Velocity = Vector2.zero;
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        if (elapsedTime < 2f)
        {
            enemy.transform.localScale = Vector2.Lerp(enemy.transform.localScale, Vector2.zero,
                enemy.fallingSpeed * Time.fixedDeltaTime);
            enemy.transform.position = Vector2.Lerp(enemy.transform.position, enemy.FallTargetPosition,
                enemy.fallingSpeed * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
        }
        else
        {
            Object.Destroy(enemy.gameObject);
        }
    }

    public override void Exit()
    {
    }
}