using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    public List<TutorialAction> tutorialActions;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var player = GameManager.Instance.GetPlayerController();
        player.DeactivateInput();

        StartCoroutine(StartActionSequence());
    }

    private IEnumerator StartActionSequence()
    {
        foreach (var action in tutorialActions)
        {
            action.Init();
            yield return StartCoroutine(action.Execute());
        }

        GameManager.Instance.GetPlayerController().ActivateInput();

        Destroy(gameObject);
    }
}