using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuTextController : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text text;

    public string titleText;
    [TextArea(3, 5)]
    public string[] sentences;
    public List<float> durations;
    
    private Queue<string> _sentences;

    void Start()
    {
        title.text = titleText;
        _sentences = new Queue<string>();
        foreach (var sentence in sentences) _sentences.Enqueue(sentence);
        StartCoroutine(StartDisplay());
    }

    private IEnumerator StartDisplay()
    {
        text.text = "";
        var durationIterator = 0;
        
        while (_sentences.Count > 0)
        {
            var displaySentence = _sentences.Dequeue();
            var duration = durations[durationIterator];
            durationIterator++;
            yield return StartCoroutine(DisplaySentence(displaySentence, duration));
        }
        End();
    }


    private IEnumerator DisplaySentence(string sentence, float seconds)
    {
        float t = 0;
        Color startColor = new Color32(255, 255, 255, 0);
        Color endColor = new Color32(255, 255, 255, 255);

        text.text = sentence;
        text.color = startColor;

        while (t < 1)
        {
            text.color = Color.Lerp(startColor, endColor, t);
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(seconds);
        t = 0;
            
        while (t < 1)
        {
            text.color = Color.Lerp(endColor, startColor, t);
            t += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }

    private void End()
    {
        _sentences.Clear();
        if (GameManager.Instance.CurrentLevelIndex == 1)
            GameManager.Instance.LoadNextLevel();
        else
        {
            GameManager.Instance.LoadLevel(0);
        }
    }
}