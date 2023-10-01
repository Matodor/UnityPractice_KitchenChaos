using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField]
    private List<KitchenObjectSO> _validIngredients = new();

    private List<KitchenObjectSO> _ingredients = new();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (_validIngredients.Contains(kitchenObjectSO) == false)
            return false;

        if (_ingredients.Contains(kitchenObjectSO))
            return false;

        _ingredients.Add(kitchenObjectSO);
        return true;
    }
}
