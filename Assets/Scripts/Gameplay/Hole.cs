using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        print("Contact with " + other);
        MovingObjectController otherController = other.GetComponentInParent<MovingObjectController>();
        if (otherController != null && otherController is IGrounded)
        {
            otherController.FallTargetPosition = transform.position;
            otherController.Fall();
        }
    }
}
