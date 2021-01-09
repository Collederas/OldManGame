using System;
using UnityEngine;

[CreateAssetMenu(fileName = "IntegerValue", menuName = "Variables/Integer")]
public class IntegerVariable : ScriptableObject
{
    public event Action<int> ValueChanged;

    public int value;
    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            ValueChanged?.Invoke(value);
        }
    }
}