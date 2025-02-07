using UnityEngine;

public abstract class BaseItemSOItemScript : MonoBehaviour
{
    public ScriptableObject BaseItemSO;
    private ItemInventory InvOwner;

    [SerializeField] private string itemName;
    private int count => InvOwner.ItemInv[itemName];

    protected virtual void Awake()
    {
        if (!gameObject.GetComponent<ItemInventory>(out InvOwner))
        {
            BaseItemSO.RemoveItemScripts(gameObject);
        }
        InvOwner.OnInventoryDirtied += UpdateOnListDirtied;
        InvOwner.OnInventoryDestroyed += OnInvOwnerDestroyed;
    }

    protected virtual void OnDestroy()
    {
        InvOwner.OnInventoryDirtied -= UpdateOnListDirtied;
        InvOwner.OnInventoryDestroyed -= OnInvOwnerDestroyed;
    }

    protected virtual void Update();

    protected virtual void UpdateOnListDirtied(ItemInventory DirtyInv)
    {   // Updates whenever their Inventory is modified
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

    protected virtual void OnInvOwnerDestroyed(ItemInventory destroyedInv)
    {   // Destroy self if somehow only the ItemInventory they belong to is destroyed
        Destroy(this);
    }
}