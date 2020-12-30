using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoostCounter : MonoBehaviour
{
    public TMP_Text counter;
    private Animator _animator;
    private static readonly int Activate = Animator.StringToHash("Activate");

    public void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetCount(int count)
    {
        counter.text = count.ToString();
    }

    public void Appear()
    {
        _animator.SetTrigger(Activate);
    }
}
