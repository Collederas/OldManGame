using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        var groundedObj = other.GetComponent<IGrounded>();
        groundedObj?.Fall(transform.position);
    }
}
