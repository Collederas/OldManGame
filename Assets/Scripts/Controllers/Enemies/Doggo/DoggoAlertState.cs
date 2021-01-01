using System;
using System.Collections;
using UnityEngine;

public class DoggoAlertState : DoggoBaseState
{
    private float _alertDuration;
    private float _elapsedTime;

    public DoggoAlertState(DoggoController doggo) : base(doggo)
    {
    }

    public override void Enter()
    {
        _alertDuration = doggo.GetComponentInChildren<Animator>().GetCurrentAnimatorClipInfo(0).Length;
    }

    public override void Update()
    {
        if (_elapsedTime < _alertDuration)
        {
            _elapsedTime += Time.deltaTime;
            return;
        }
        doggo.ChangeState(doggo.chaseState);
    }

    public override void FixedUpdate()
    {
    }

    public override void Exit()
    {
    }
}