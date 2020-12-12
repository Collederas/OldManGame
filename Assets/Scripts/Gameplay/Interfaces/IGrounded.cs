using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Grounded object can fall
public interface IGrounded
{
   Vector2 FallTargetPosition { get; set; }
   void Fall();
}
