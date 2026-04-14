using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct Item
{
    [SerializeField] public ItemType itemType;
    [SerializeField] public GameObject sceneObject;
    [Space(3), SerializeField] public UnityEvent<Item> OnCountChanged;
};

public class ItemController : MonoBehaviour
{
    public UnityEvent onBuy;
    [Space(3)] public UnityEvent onSpend;
    [Space(3)] public UnityEvent onUpdateItems;
    [Space(5)] public List<Item> items;
    private ItemStorage itemStorage;


    private void Start()
    {
        itemStorage = ItemStorage.Instance;
        UpdateItems();
    }

// ------------------------------------------------------------------------------------------------

    public void UpdateItems()
    {
        foreach(var item in items)
            item.sceneObject.gameObject.SetActive(itemStorage.GetItem(item.itemType).IsPresent);
        onUpdateItems.Invoke();
    }

    public bool BuyItem(ItemType it)
    {
        if (itemStorage.BuyItem(it))
        {
            UpdateItems();
            onBuy.Invoke();
            return true;
        }

        return false;
    }

    public bool SpendItem(ItemType it)
    {
        if (itemStorage.SpendItem(it))
        {
            UpdateItems();
            onSpend.Invoke();
            return true;
        }
        return false;
    }
}
