using UnityEditor.UIElements;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyFOV : MonoBehaviour
{
    // Only fire message for collisions with this tag
    public string detectionTag;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(detectionTag))
            SendMessageUpwards("OnFOVTagDetected");
    }
}
