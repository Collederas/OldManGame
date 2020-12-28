using System.Collections;
using UnityEngine;

public abstract class TutorialAction : ScriptableObject
{
    public abstract void Init();
    public abstract IEnumerator Execute();
}