using UnityEngine;

[CreateAssetMenu]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO Input => _input;
    public KitchenObjectSO Output => _output;
    public float BurningTimer => _burningTimer;

    [SerializeField]
    private KitchenObjectSO _input;

    [SerializeField]
    private KitchenObjectSO _output;

    [SerializeField]
    private int _burningTimer;
}
