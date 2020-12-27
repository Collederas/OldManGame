using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    public List<TutorialAction> tutorialActions;

    private GameManager _gameManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _gameManager = FindObjectOfType<GameManager>();
        
        var player = _gameManager.GetPlayer();
        player.DeactivateInput();
        
        StartCoroutine(StartActionSequence());
    }

    private IEnumerator StartActionSequence()
    {
        foreach (var action in tutorialActions)
        {
            action.Init(_gameManager);
            yield return StartCoroutine(action.Execute());
        }
        _gameManager.GetPlayer().ActivateInput();

        Destroy(gameObject);
    }
}