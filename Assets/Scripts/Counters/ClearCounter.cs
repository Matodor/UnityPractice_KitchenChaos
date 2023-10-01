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
            else
            {
                if (player.GetKitchenObject() is PlateKitchenObject playerPlate)
                {
                    if (playerPlate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().Destroy();
                    }
                }
                else
                {
                    if (GetKitchenObject() is PlateKitchenObject plateKitchenObject)
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().Destroy();
                        }
                    }

                }
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
