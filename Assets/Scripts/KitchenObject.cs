using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]
    private KitchenObjectSO _kitchenObjectSO;

    private IKitchenObjectParent _kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return _kitchenObjectSO;
    }

    public KitchenObject SetParent(IKitchenObjectParent parent)
    {
        if (parent.HasKitchenObject())
            Debug.LogError("IKitchenObjectParent already has a KitchenObject!");

        _kitchenObjectParent?.ClearKitchenObject();
        _kitchenObjectParent = parent;
        _kitchenObjectParent.SetKitchenObject(this);

        transform.parent = parent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;

        return this;
    }

    public IKitchenObjectParent GetParent()
    {
        return _kitchenObjectParent;
    }

    public void Destroy()
    {
        _kitchenObjectParent?.ClearKitchenObject();
        MonoBehaviour.Destroy(gameObject);
    }

    public static KitchenObject Spawn(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent parent)
    {
        return Instantiate(kitchenObjectSO.Prefab, parent.GetKitchenObjectFollowTransform())
            .GetComponent<KitchenObject>()
            .SetParent(parent);
    }
}
