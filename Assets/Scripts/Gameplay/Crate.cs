﻿using UnityEngine;

public class Crate : MonoBehaviour, IGrounded
{
    public float fallingSpeed = 2f;

    private bool _fall;
    private float _fallingTime;
    public Vector2 FallTargetPosition { get; set; }

    public void FixedUpdate()
    {
        if (!_fall) return;
        if (_fallingTime < 2f)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, fallingSpeed * Time.fixedDeltaTime);
            transform.position =
                Vector2.Lerp(transform.position, FallTargetPosition, fallingSpeed * Time.fixedDeltaTime);
            _fallingTime += Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Fall(Vector2 fallTargetPosition)
    {
        FallTargetPosition = fallTargetPosition;
        _fall = true;
    }
}