using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField]
    private KitchenObjectSO _kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject() == false)
            {
                GetKitchenObject().SetParent(player);
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetParent(this);
            }
        }
    }
}
