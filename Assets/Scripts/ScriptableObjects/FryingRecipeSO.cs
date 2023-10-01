using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO Input => _input;
    public KitchenObjectSO Output => _output;
    public float FryingTimer => _fryingTimer;

    [SerializeField]
    private KitchenObjectSO _input;

    [SerializeField]
    private KitchenObjectSO _output;

    [SerializeField]
    private int _fryingTimer;
}
