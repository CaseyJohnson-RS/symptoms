using UnityEngine;

public enum ItemType
{
    TICKET,
    GUM,
    PLAYER, // Q: Переименовать в BOOMBOX из-за разночтения
    LUCKY_TICKET,
    FILM
}

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Scriptable Objects/ItemInfo")]
public class ItemInfo : ScriptableObject // Q: Почему "ItemInfo", а не "ItemData"?
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