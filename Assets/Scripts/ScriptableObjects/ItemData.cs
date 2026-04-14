using UnityEngine;

public enum ItemType
{
    TICKET,
    GUM,
    BOOMBOX,
    LUCKY_TICKET,
    FILM
}

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Objects/ItemInfo")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField, Multiline] private string description;
    [SerializeField] private bool isPurchaseOnce;
    [SerializeField, Range(0, 1000)] private int cost; 

    public ItemType Type => type;
    public string Name => itemName;
    public Sprite Sprite => sprite;
    public string Description => description;
    public bool IsPurchaseOnce => isPurchaseOnce;
    public int Cost => cost;
}