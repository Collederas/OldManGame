using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyFOV : MonoBehaviour
{
    // Only fire message for collisions with this tag
    public string detectionTag;
    public Animator animator;
    private static readonly int Alert = Animator.StringToHash("Alert");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(detectionTag))
        {
            StartCoroutine(PlayClip(.8f));
            animator.SetTrigger(Alert);
            SendMessageUpwards("OnFOVTagDetected");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(detectionTag))
            SendMessageUpwards("OnFOVTagLost");
    }
    
    protected IEnumerator PlayClip(float delay = 0f)
    {
        var audioSource = GetComponent<AudioSource>();
        yield return new WaitForSeconds(delay);
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }
}