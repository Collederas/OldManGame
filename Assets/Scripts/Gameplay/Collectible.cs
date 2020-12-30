using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public abstract class Collectible : MonoBehaviour
{
    [HideInInspector]
    public bool isActive;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        SetObjectActive(false);
        Collect(other.GetComponent<PlayerController>());
        StartCoroutine(PlayClip());
    }

    protected void SetObjectActive(bool value)
    {
        GetComponent<Collider2D>().enabled = value;
        GetComponent<SpriteRenderer>().enabled = value;
        isActive = false;
    }

    protected IEnumerator PlayClip()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
    }
    protected abstract void Collect(PlayerController playerController);
}
