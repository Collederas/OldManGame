using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectController : MonoBehaviour
{
    public float maxSpeed = 10;
    public Vector2 Velocity { get; set; }
    protected Vector2 acceleration;

    void Start()
    {
    }

    protected virtual void CalcVelocity()
    {
        Velocity = acceleration * maxSpeed;
        Velocity = Vector2.ClampMagnitude(Velocity, maxSpeed);
    }

    protected void PerformMovement(Vector2 delta)
    { 
        // Transform is only a Vector3 so build delta as a 3D vector. 
        transform.position = transform.position + new Vector3(delta.x, delta.y, 0);
    }

    protected virtual void Update()
    {           
        /* In CalcVelocity switch between states and calculate velocity based 
        on the active one. */
        CalcVelocity();
    }

    void FixedUpdate()
    {   
        var delta = Velocity * Time.deltaTime;
        PerformMovement(delta);
    }
}
