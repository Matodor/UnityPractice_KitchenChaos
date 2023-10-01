using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler PlateSpawned;
    public event EventHandler PlateTaken;

    [SerializeField]
    private KitchenObjectSO _plateKitchenObjectSO;

    private float _spawnPlateTimer;
    private float _spawnPlateTimerMax = 4f;

    private int _platesSpawnedAmount;
    private int _platesSpawnedAmountMax = 4;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;

        if (_spawnPlateTimer > _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0;

            if (_platesSpawnedAmount < _platesSpawnedAmountMax)
            {
                _platesSpawnedAmount++;

                PlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() == false)
        {
            if (_platesSpawnedAmount > 0)
            {
                _platesSpawnedAmount--;

                KitchenObject.Spawn(_plateKitchenObjectSO, player);
                PlateTaken?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
