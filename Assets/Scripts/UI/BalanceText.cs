using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class BalanceText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string prefix;

    void Start()
    {
        UpdateBalance();
    }

    public void UpdateBalance()
    {
        text.text = prefix + ItemStorage.Instance.Ticket.Count.ToString() + "$";
    }
}
