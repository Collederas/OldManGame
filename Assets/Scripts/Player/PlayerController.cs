using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovingObjectController
{
    [Range(0.0f, 5.0f)]
    public float boostDuration = 1f;
    [Range(0.0f, 5.0f)]
    public float boostDistance = 5f;
    public float maxBoostingSpeed = 15f;
    public float maxWalkingSpeed = 2f;

    private Vector2 inputAcceleration;
    private Vector2 impulseAcceleration;

    public void Start()
    {
        maxSpeed = maxWalkingSpeed;
    }

    public void OnMove(InputValue value)
    {
        inputAcceleration = value.Get<Vector2>();
    }

    public void OnFire()
    {
        StartCoroutine(ApplyImpulse(Velocity.normalized * (boostDistance/boostDuration), boostDuration, maxWalkingSpeed));
    }

    protected override void CalcVelocity()
    {
        Velocity = impulseAcceleration + inputAcceleration;
        base.CalcVelocity();
    }

    private IEnumerator ApplyImpulse(Vector2 impulse, float boostDuration, float preBoostMaxSpeed)
    {
        float time = 0f;
        while (time <= boostDuration)
        {
            maxSpeed = maxBoostingSpeed;
            time += Time.fixedDeltaTime;
            impulseAcceleration = impulse;
            yield return null;
        }
        impulseAcceleration = Vector2.zero;
        maxSpeed = preBoostMaxSpeed;
    }
}
