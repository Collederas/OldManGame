using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TutorialTrigger : MonoBehaviour
{
    public List<TutorialAction> tutorialActions;
    public string playerPrefKey;
    private PlayerController _playerController;


    /* Ugly but quick solution.
       Set the Frozen flag on these enemy controllers during tutorial. */
    public EnemyStateMachineController[] enemiesToFreeze;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerPrefs.GetInt(playerPrefKey) == 1) return;
        if (!other.CompareTag("Player")) return;

        _playerController = other.GetComponent<PlayerController>();
        _playerController.gameObject.layer = LayerMask.NameToLayer("Invincible");

        StartCoroutine(StartActionSequence());
        PlayerPrefs.SetInt(playerPrefKey, 1);
    }

    private IEnumerator StartActionSequence()
    {
        _playerController.DeactivateInput();
        foreach (var enemy in enemiesToFreeze)
        {
            enemy.frozen = true;
        }

        foreach (var action in tutorialActions)
        {
            action.Init();
            yield return StartCoroutine(action.Execute());
        }
        
        _playerController.ActivateInput();
        _playerController.gameObject.layer = LayerMask.NameToLayer("Damageable");
        foreach (var enemy in enemiesToFreeze)
        {
            enemy.frozen = false;
        }
        
        Destroy(gameObject);
    }
}