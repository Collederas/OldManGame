using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    public List<TutorialAction> tutorialActions;
    private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _playerController = other.GetComponent<PlayerController>();

        StartCoroutine(StartActionSequence());
    }

    private IEnumerator StartActionSequence()
    {
        _playerController.DeactivateInput();

        foreach (var action in tutorialActions)
        {
            action.Init();
            yield return StartCoroutine(action.Execute());
        }
        _playerController.ActivateInput();

        Destroy(gameObject);
    }
}