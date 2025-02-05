using UnityEngine;

public abstract virtual class BaseItemScript : MonoBehaviour
{
    public ScriptableObject Base;
    private ItemInventory InvOwner;

    [SerializeField] private string itemName;
    private int count;

    void Awake()
    {
        InvOwner = this.GameObject.GetComponent<ItemInventory>();
    }

    private void UpdateOnListDirtied(ItemInventory DirtyInv)
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

    //Item Scripts destroy themself if their ItemInventory monoscript is destroyed
    private void OnInvOwnerDestroyed(ItemInventory destroyedInv)
    {
        if (destroyedInv == InvOwner)
        {
            Destroy(this);
        }
    }
}