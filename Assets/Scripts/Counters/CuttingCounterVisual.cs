using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField]
    private CuttingCounter _counter;

    private Animator _animator;
    private static readonly int TriggerCut = Animator.StringToHash("Cut");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _counter.OnCut += CounterOnCut;
    }

    private void CounterOnCut(object sender, EventArgs e)
    {
        _animator.SetTrigger(TriggerCut);
    }
}
