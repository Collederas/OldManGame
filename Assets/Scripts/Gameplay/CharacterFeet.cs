using UnityEngine;

public class CharacterFeet : MonoBehaviour, IGrounded
{
    private MovingObjectController _controller;
    private void Start()
    {
        _controller = GetComponentInParent<MovingObjectController>();
    }   

    public void Fall(Vector2 fallTargetPosition)
    {
        _controller.Fall(fallTargetPosition);
    }
}
