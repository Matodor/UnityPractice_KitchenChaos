using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO Input => _input;
    public KitchenObjectSO Output => _output;

    [SerializeField]
    private KitchenObjectSO _input;

    [SerializeField]
    private KitchenObjectSO _output;
}
