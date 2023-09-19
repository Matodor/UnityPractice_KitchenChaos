using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField]
    private CuttingRecipeSO[] _cuttingRecipesSO;

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            var kitchenObject = GetKitchenObject();
            var output = FindRecipeOutputForInput(kitchenObject.GetKitchenObjectSO());

            if (output != null)
            {
                kitchenObject.Destroy();
                KitchenObject.Spawn(output, this);
            }
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
                if (HasCuttingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetParent(this);
                }
            }
        }
    }

    private bool HasCuttingRecipe(KitchenObjectSO input)
    {
        return _cuttingRecipesSO.Any(so => so.Input == input);
    }

    [CanBeNull]
    private KitchenObjectSO FindRecipeOutputForInput(KitchenObjectSO input)
    {
        return _cuttingRecipesSO.FirstOrDefault(so => so.Input == input)?.Output;
    }
}
