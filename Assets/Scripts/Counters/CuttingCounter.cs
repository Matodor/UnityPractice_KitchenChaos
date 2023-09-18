public class CuttingCounter : BaseCounter
{
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            GetKitchenObject().Destroy();
        }
    }

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
