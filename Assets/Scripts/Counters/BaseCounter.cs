using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]
    private Transform _counterTopPoint;

    private KitchenObject _kitchenObject;

    public virtual void InteractAlternate(Player player)
    {

    }

    public virtual void Interact(Player player)
    {

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
