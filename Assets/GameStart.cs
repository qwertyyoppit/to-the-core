using UnityEngine;

public class GameStart : MonoBehaviour
{
    private void Awake()
    {
        foreach (BaseItemInfo itemSO in Resources.FindObjectsOfTypeAll<BaseItemInfo>())
        {
            Items.ItemID.Add(itemSO.name, itemSO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
