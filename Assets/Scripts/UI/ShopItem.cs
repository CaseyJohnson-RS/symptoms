using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private ItemController ic;
    [SerializeField] private ItemData itemData;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private UnityEvent onBuy;
    [SerializeField] private UnityEvent onBuyError;

    [SerializeField] private float typingDelay = 0.03f;

    private Coroutine typingCoroutine;

    private void Start()
    {
        if (!ic)
        {
            GameObject controller = GameObject.FindWithTag("ItemController");
            if (controller)
                ic = controller.GetComponent<ItemController>();
        }

        UpdateData();
    }

    private void UpdateData()
    {
        StartTyping(descriptionText, itemData.Description);
        costText.text = itemData.Cost + "$";
    }

    private void StartTyping(TextMeshProUGUI target, string text)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(target, text));
    }

    private IEnumerator TypeText(TextMeshProUGUI target, string text)
    {
        target.text = text;
        target.ForceMeshUpdate();
        target.maxVisibleCharacters = 0;

        int totalCharacters = target.textInfo.characterCount;

        for (int i = 0; i <= totalCharacters; i++)
        {
            target.maxVisibleCharacters = i;
            yield return new WaitForSeconds(typingDelay);
        }
    }

    public void Buy()
    {
        if (ic.BuyItem(itemData.Type))
        {
            onBuy.Invoke();
        }
        else
        {
            onBuyError.Invoke();
        }
    }
}