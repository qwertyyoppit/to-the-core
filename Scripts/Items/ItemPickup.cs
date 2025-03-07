using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    public BaseItem Item;

    public int Amount = 1;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (!collision.gameObject.TryGetComponent<ItemInventory>(out ItemInventory Inv))
        {
            return;
        }
        if (Amount == 0)
        {
            return;
        }
        if (Amount > 0)
        {
            Inv.AddItem(Item.ItemName, Amount);
        }
        if (Amount < 0) //todo: not sure if removeitem is working, or if this is getting called correctly. fix stat reference shenanigans first
        {
            Inv.RemoveItem(Item.ItemName, Mathf.Abs(Amount));
        }
    }
}
