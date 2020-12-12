using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObjectController : MonoBehaviour, IGrounded
{
    public Vector2 Velocity { get; set; }
    public Vector2 Impulse { get; set; }

    // Where the fall animation should be aiming to.
    public Vector2 FallTargetPosition { get; set; }

    public float maxSpeed = 2f;

    protected void PerformMovement(Vector2 delta)
    { 
        // Transform is only a Vector3 so build delta as a 3D vector. 
        transform.position = transform.position + new Vector3(delta.x, delta.y, 0);
    }

    protected virtual void Update()
    {
        Velocity = Vector2.ClampMagnitude(Velocity, maxSpeed);
    }

    protected virtual void FixedUpdate()
    {
        var delta = Velocity * Time.deltaTime;
        PerformMovement(delta);
    }

    public virtual void Fall()
    {

    }
}
