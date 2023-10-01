using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField]
    private GameObject _stoveOnGameObject;

    [SerializeField]
    private GameObject _particlesGameObject;

    [SerializeField]
    private StoveCounter _stoveCounter;

    private void Start()
    {
        _stoveCounter.StageChanged += StoveCounterOnStageChanged;
    }

    private void StoveCounterOnStageChanged(object sender, StoveCounter.StageChangedEventArgs e)
    {
        var show = e.State is StoveCounter.State.Frying or StoveCounter.State.Fried;

        _stoveOnGameObject.SetActive(show);
        _particlesGameObject.SetActive(show);
    }
}
