using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    public ScriptableObject ItemBase;

    public int amount = 1;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<ItemInventory>(out ItemInventory Inv))
        {
            Inv.AddItem(ItemBase.name, amount);
        }
    }
}
