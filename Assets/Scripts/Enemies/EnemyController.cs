using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovingObjectController
{
    protected enum State
    {
        Idle,
        Attack,
        Chase
    }

    protected State currentState;

    private System.Reflection.MethodInfo currentStateMethodCached = null;
    protected virtual void Start()
    {
        currentState = State.Idle;
        ChangeState(currentState);
    }

    protected override void Update()
    {
        ChangeState(currentState);
        base.Update();
    }

    protected virtual void IdleState()
    {
        Velocity = Vector2.zero;
    }

    protected virtual void AttackState()
    {

    }

    protected virtual void ChaseState()
    {

    }

    protected void ChangeState(State newState)
    {
        if (currentStateMethodCached == null)
        {
            string methodName = newState.ToString() + "State";
            System.Reflection.MethodInfo stateMethod =
                GetType().GetMethod(
                    methodName,
                    System.Reflection.BindingFlags.NonPublic |
                    System.Reflection.BindingFlags.Instance);
            currentStateMethodCached = stateMethod;
        }
        else
        {
            currentStateMethodCached.Invoke(this, null);
            currentStateMethodCached = null;
        }
    }
}
