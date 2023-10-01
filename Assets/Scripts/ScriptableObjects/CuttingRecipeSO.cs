using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO Input => _input;
    public KitchenObjectSO Output => _output;
    public int CuttingAmount => _cuttingAmount;

    [SerializeField]
    private KitchenObjectSO _input;

    [SerializeField]
    private KitchenObjectSO _output;

    [SerializeField]
    private int _cuttingAmount;
}
