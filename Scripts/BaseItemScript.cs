using UnityEngine;

public abstract virtual class BaseItemSOItemScript : MonoBehaviour
{
    public ScriptableObject BaseItemSO;
    private ItemInventory InvOwner;

    [SerializeField] private string itemName;
    private int count => InvOwner.ItemInv[itemName];

    virtual void Awake()
    {
        InvOwner = this.GameObject.GetComponent<ItemInventory>();
        if (InvOwner == null)
        {
            BaseItemSO.RemoveItemScripts
        }
        InvOwner.OnItemInvDestroyed += OnInvOwnerDestroyed;
    }

    virtual void OnDestroy()
    {
        InvOwner.OnItemInvDestroyed -= OnInvOwnerDestroyed;
    }

    virtual void Update()
    {

    }

    private virtual void UpdateOnListDirtied(ItemInventory DirtyInv)
    {
        if (DirtyInv != InvOwner)
        {
            return;
        }
        if (!InvOwner.DirtyItems.Contains<string>(itemName))
        {
            return;
        }
        InvOwner.Undirty(itemName);
    }

    private virtual void OnInvOwnerDestroyed(ItemInventory destroyedInv)
    {
        // Destroy self if somehow only the ItemInventory they belong to is destroyed
        // If I understand correctly, it only subscribes to the ItemInventory's non-static destroyed event
        Destroy(this);
    }
}