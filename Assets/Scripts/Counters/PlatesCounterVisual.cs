using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField]
    private PlatesCounter _platesCounter;

    [SerializeField]
    private Transform _counterTopPoint;

    [SerializeField]
    private Transform _plateVisualPrefab;

    private readonly List<GameObject> _plateVisualGameObjects = new ();

    private void Start()
    {
        _platesCounter.PlateSpawned += PlatesCounterOnPlateSpawned;
        _platesCounter.PlateTaken += PlatesCounterOnPlateTaken;
    }

    private void PlatesCounterOnPlateTaken(object sender, EventArgs e)
    {
        var plateVisualIndex = _plateVisualGameObjects.Count - 1;
        var plateVisualGameObject = _plateVisualGameObjects[plateVisualIndex];

        _plateVisualGameObjects.RemoveAt(plateVisualIndex);

        Destroy(plateVisualGameObject);
    }

    private void PlatesCounterOnPlateSpawned(object sender, EventArgs e)
    {
        var plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);
        var plateOffsetY = 0.1f;

        plateVisualTransform.localPosition =
            new Vector3(0, plateOffsetY * _plateVisualGameObjects.Count, 0);

        _plateVisualGameObjects.Add(plateVisualTransform.gameObject);
    }
}
