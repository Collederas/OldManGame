using System.Collections;
using UI;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDialogue", menuName = "Tutorial Actions/Dialogue")]

public class T_Dialogue : TutorialAction
{
    public Dialogue dialogue;
    
    private bool _dialogueRunning = false;
    private DialogueManager _dialogueManager;
    private PlayerController _player;
    public override void Init(GameManager gameManager)
    {
        _player = gameManager.GetPlayer();
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _dialogueRunning = true;
    }

    public override IEnumerator Execute()
    {
        _dialogueManager.DialogueEnded += OnDialogueEnded;
        _dialogueManager.StartDialogue(dialogue);
        while(_dialogueRunning)
            yield return null;
    }

    private void OnDialogueEnded()
    {
        _dialogueRunning = false;
    }
}

