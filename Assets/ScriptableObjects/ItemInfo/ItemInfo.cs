using UnityEngine;

public enum ItemType
{
    TICKET,
    GUM,
    PLAYER,
    LUCKY_TICKET,
    FILM
}

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Objects/ItemInfo")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string description;
    [SerializeField] private bool isPurchaseOnce;

    public ItemType Type => type;
    public string Name => itemName;
    public Sprite Sprite => sprite;
    public string Description => description;
    public bool IsPurchaseOnce => isPurchaseOnce;
}