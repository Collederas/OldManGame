using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingObjectController))]
[RequireComponent(typeof(Animator))]

public class Animate : MonoBehaviour
{
    private MovingObjectController movingObject;
    private Animator animator;
    private Vector2 velocityNormalized;

    void Start()
    {
        animator = GetComponent<Animator>();
        movingObject = GetComponent<MovingObjectController>();
    }

    void Update()
    {
        velocityNormalized = movingObject.Velocity.normalized;

        var vertical = Vector2.Dot(velocityNormalized, Vector2.up);
        var horizontal = Vector2.Dot(velocityNormalized, Vector2.right);
        var speed = movingObject.Velocity.magnitude / Time.deltaTime;
        
        if (horizontal != 0 | vertical != 0)
        {
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
        animator.SetFloat("Speed", speed);
    }
}
