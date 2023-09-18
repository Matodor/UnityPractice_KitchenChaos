using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField]
    private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
            return;

        KitchenObject.Spawn(_kitchenObjectSO, player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
