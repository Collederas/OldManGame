using System;
using UI;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        if (dialogueManager == null)
        {
            Debug.LogWarning("DialogueManager dependency is empty. Trying to locate a DialogueManager" +
                             " object in current Scene");
            dialogueManager = FindObjectOfType<DialogueManager>();
            if (dialogueManager == null)
            {
                Debug.LogWarning("Can't locate a DialogueManager in current scene");
                return;
            }
        }
        dialogueManager.DialogueEnded += OnDialogueEnded;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _gameManager.GetPlayer().DeactivateInput();
        dialogueManager.StartDialogue(dialogue);
    }

    private void OnDialogueEnded()
    {
        _gameManager.GetPlayer().ActivateInput();
        Destroy(gameObject);
    }
}