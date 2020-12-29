using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [TextArea(3, 5)] public string[] sentences;
    }
}