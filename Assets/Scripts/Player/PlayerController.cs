using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MovingObjectController
{
    void Start()
    {
        
    }

    public void OnMove(InputValue value)
    {
        acceleration = value.Get<Vector2>();
    }
}
