using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    [Serializable]
    public struct Item
    {
        [SerializeField] public ItemInfo Info;
        [SerializeField] public int Count;
        [SerializeField] public UnityEvent<Item> OnCountChanged;

        public bool IsPresent()
        {
            return Count > 0;
        }
    }

    [SerializeField] private Item ticket;
    [SerializeField] private List<Item> items;

    public static ItemManager Instance { get; private set; }

    public void AddTickets(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentException(nameof(count));
        }

        ticket.Count += count;
        ticket.OnCountChanged.Invoke(ticket);
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
        ticket.OnCountChanged.Invoke(ticket);
    }

    public void AddItem(ItemType type)
    {
        Item item = items.Find(item => item.Info.Type == type);
        if (item.Info.IsPurchaseOnce && item.IsPresent())
        {
            throw new InvalidOperationException($"Предмет {item.Info.Name} уже куплен");
        }

        item.Count++;
        item.OnCountChanged.Invoke(item);
    }

    public void RemoveItem(ItemType type)
    {
        Item item = items.Find(item => item.Info.Type == type);
        if (item.Info.IsPurchaseOnce)
        {
            throw new InvalidOperationException($"Предмет {item.Info.Name} нельзя потратить");
        }

        if (!item.IsPresent())
        {
            throw new InvalidOperationException($"Предмет {item.Info.Name} отсутствует");
        }

        item.Count--;
        item.OnCountChanged.Invoke(item);
    }

    public void AddListenerOnItem(ItemType type, UnityAction<Item> listener)
    {
        Item item = items.Find(item => item.Info.Type == type);
        if (item.Info == null)
        {
            throw new ArgumentException($"Предмет типа {type} не найден");
        }

        item.OnCountChanged.AddListener(listener);
    }

    public void RemoveListenerFromItem(ItemType type, UnityAction<Item> listener)
    {
        Item item = items.Find(item => item.Info.Type == type);
        if (item.Info == null)
        {
            throw new ArgumentException($"Предмет типа {type} не найден");
        }

        item.OnCountChanged.RemoveListener(listener);
    }

    public void AddListenerOnTickets(UnityAction<Item> listener)
    {
        ticket.OnCountChanged.AddListener(listener);
    }

    public void RemoveListenerFromTickets(UnityAction<Item> listener)
    {
        ticket.OnCountChanged.RemoveListener(listener);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}