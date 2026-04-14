using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ItemStorage : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        [SerializeField] public ItemData Data;
        [SerializeField] public int Count;
        public bool IsPresent => Count > 0;
    }

    [SerializeField] private Item ticket;
    [SerializeField] private List<Item> items;

    public Item Ticket => ticket;
    public List<Item> Items => items;

    public static ItemStorage Instance { get; private set; }

    public void AddTickets(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException(nameof(count));
        }

        ticket.Count += count;
    }

    public void RemoveTickets(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException(nameof(count));
        }

        if (ticket.Count < count)
        {
            throw new ArgumentException($"Количество билетов {ticket.Count} меньше, чем {count}");
        }

        ticket.Count -= count;
    }

    public Item GetItem(ItemType type)
    {
        Item item = items.Find(item => item.Data.Type == type);
        return item;
    }

    public void AddItem(ItemType type)
    {
        Item item = items.Find(item => item.Data.Type == type);
        if (item.Data.IsPurchaseOnce && item.IsPresent)
        {
            throw new InvalidOperationException($"Предмет {item.Data.Name} уже куплен");
        }
        
        item.Count++;
    }

    public void RemoveItem(ItemType type)
    {
        Item item = items.Find(item => item.Data.Type == type);
        if (item.Data.IsPurchaseOnce)
        {
            throw new InvalidOperationException($"Предмет {item.Data.Name} нельзя потратить");
        }

        if (!item.IsPresent)
        {
            throw new InvalidOperationException($"Предмет {item.Data.Name} отсутствует");
        }

        item.Count--;
    }

    public bool BuyItem(ItemType type) // Safe function
    {
        Item item = GetItem(type);

        if (item.IsPresent && item.Data.IsPurchaseOnce) // Early check for transaction consistancy
            return false;
        
        try
        {
            RemoveTickets(item.Data.Cost);
            AddItem(type); // Won't throw any exception
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }

    public bool SpendItem(ItemType type)  // Safe function
    {
        Item item = GetItem(type);
        if (item.IsPresent)
        {
            --item.Count;
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}