using System.Collections;
using UnityEngine;

public abstract class TutorialAction : ScriptableObject
{
    public abstract void Init(GameManager gameManager);
    public abstract IEnumerator Execute();
}
