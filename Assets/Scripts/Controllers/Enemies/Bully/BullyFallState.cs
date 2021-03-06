﻿using UnityEngine;

public class BullyFallState : BullyBaseState
{
    private float elapsedTime;

    public BullyFallState(BullyController enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        elapsedTime = 0f;
    }

    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        if (elapsedTime < 2f)
        {
            bully.transform.localScale = Vector2.Lerp(bully.transform.localScale, Vector2.zero,
                bully.fallingSpeed * Time.fixedDeltaTime);
            bully.transform.position = Vector2.Lerp(bully.transform.position, bully.FallTargetPosition,
                bully.fallingSpeed * Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
        }
        else
        {
            bully.Die();
        }
    }

    public override void Exit()
    {
    }
}