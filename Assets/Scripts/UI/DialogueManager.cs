using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    [Serializable]
    public enum DialogueSpeed
    {
        Slow = 4,
        Medium = 10,
        Fast = 30
    }

    public class DialogueManager : MonoBehaviour
    {
        private static readonly int OpenTextBox = Animator.StringToHash("IsOpen");

        public DialogueSpeed dialogueSpeed = DialogueSpeed.Medium;

        public Animator animator;
        public TMP_Text displayText;
        public InputAction nextSentence;

        private Queue<string> _sentences;

        private void Start()
        {
            _sentences = new Queue<string>();
            nextSentence.performed += OnNextSentencePerformed;

            if (animator == null)
                Debug.LogWarning("Animator dependency is null. Dialogues might not show correctly");
        }

        public event Action DialogueEnded;

        private void OnNextSentencePerformed(InputAction.CallbackContext context)
        {
            DisplayNextSentence();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            StartCoroutine(StartDialogueRoutine(dialogue));
        }

        private void DisplayNextSentence()
        {
            displayText.text = "";

            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            var displaySentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(displaySentence));
        }

        private float SpeedToSeconds()
        {
            return 1 / (float) dialogueSpeed;
        }

        private void EndDialogue()
        {
            nextSentence.Disable();
            if (animator)
                animator.SetBool(OpenTextBox, false);

            DialogueEnded?.Invoke();
        }

        private IEnumerator TypeSentence(string sentence)
        {
            var seconds = SpeedToSeconds();

            foreach (var letter in sentence.ToCharArray())
            {
                displayText.text += letter;
                yield return new WaitForSeconds(seconds);
            }
        }

        public IEnumerator StartDialogueRoutine(Dialogue dialogue)
        {
            displayText.text = "";

            yield return DisplayTextBox();

            nextSentence.Enable();
            _sentences.Clear();

            if (animator)
                animator.SetBool(OpenTextBox, true);

            foreach (var sentence in dialogue.sentences) _sentences.Enqueue(sentence);
            DisplayNextSentence();
        }

        private IEnumerator DisplayTextBox()
        {
            if (animator)
                animator.SetBool(OpenTextBox, true);
            yield return new WaitForSeconds(0.7f);
        }
    }
}