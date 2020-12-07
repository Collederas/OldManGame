using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    public Vector2 Velocity { get; set; }
    public Vector2 Impulse { get; set; }
    protected float maxSpeed = 2f;

    void Start()
    {
        Impulse = Vector2.zero;
    }

    protected virtual void CalcVelocity()
    {
        Velocity = Vector2.ClampMagnitude(Velocity, maxSpeed);
    }

    protected void PerformMovement(Vector2 delta)
    { 
        // Transform is only a Vector3 so build delta as a 3D vector. 
        transform.position = transform.position + new Vector3(delta.x, delta.y, 0);
    }

    protected virtual void Update()
    {           
        CalcVelocity();
    }

    protected virtual void FixedUpdate()
    {
        var delta = Velocity * Time.deltaTime;
        PerformMovement(delta);
    }
}
