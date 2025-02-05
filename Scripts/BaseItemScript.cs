using UnityEngine;

public abstract virtual class BaseItemScript : MonoBehaviour
{
    public ScriptableObject Base;
    private MonoScript InvOwner;

    [SerializeField] private string itemName;
    private int count;

    void Awake()
    {
        InvOwner = this.GameObject.GetComponent<ItemInventory>();
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