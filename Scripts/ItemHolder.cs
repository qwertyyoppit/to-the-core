using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public ItemInventory InvOwner;

    public List<GameObject> ItemObjects = new();

    private void Awake()
    {
        InvOwner = GetComponentInParent<ItemInventory>();
        if (!InvOwner)
        {
            Destroy(gameObject);
        }
        InvOwner.Holder = this;
        InvOwner.ItemInvDestroyed += OnItemInvDestroyed;
        InvOwner.OnListDirtied += OnInvDirtied;
    }

    private void OnInvDirtied(ItemInventory Inv)
    {
        if (!InvOwner)
        {
            Destroy(this);
            return;
        }
        if (Inv != InvOwner)
        {
            return;
        }
        foreach (GameObject ItemObj in ItemObjects)
        {
            if (!ItemObj)
            {
                ItemObjects.Remove(ItemObj);
            }
            if (ItemObj.TryGetComponent<BaseItemScript>(out BaseItemScript ItemScript))
            {
                ItemScript.UpdateStats();
            }
        }
    }

    private void OnItemInvDestroyed(ItemInventory Inv)
    {
        if (Inv == InvOwner)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (InvOwner != null)
        {
            InvOwner.Holder = null;
        }
        InvOwner.ItemInvDestroyed -= OnItemInvDestroyed;
        InvOwner.OnListDirtied -= OnInvDirtied;
    }
}
