using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.ProgressChangedArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField]
    private CuttingRecipeSO[] _cuttingRecipesSO;

    private int _cuttingProgress = 0;

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            var kitchenObject = GetKitchenObject();
            var recipe = FindCuttingRecipe(kitchenObject.GetKitchenObjectSO());

            if (recipe != null)
            {
                _cuttingProgress++;

                OnCut?.Invoke(this, EventArgs.Empty);
                OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
                {
                    ProgressNormalized = (float) _cuttingProgress / recipe.CuttingAmount,
                });

                if (_cuttingProgress >= recipe.CuttingAmount)
                {
                    kitchenObject.Destroy();
                    KitchenObject.Spawn(recipe.Output, this);
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject() is PlateKitchenObject plateKitchenObject)
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().Destroy();
                    }
                }
            }
            else
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
                    _cuttingProgress = 0;

                    player.GetKitchenObject().SetParent(this);
                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
                    {
                        ProgressNormalized = 0,
                    });
                }
            }
        }
    }

    private bool HasCuttingRecipe(KitchenObjectSO input)
    {
        return _cuttingRecipesSO.Any(so => so.Input == input);
    }

    [CanBeNull]
    private CuttingRecipeSO FindCuttingRecipe(KitchenObjectSO input)
    {
        return _cuttingRecipesSO.FirstOrDefault(so => so.Input == input);
    }
}
