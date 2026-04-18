using UnityEngine;
using UnityEngine.Events;

public class OnEnableEventInvoker : MonoBehaviour
{
    [SerializeField] private UnityEvent onEnable;

    private void OnEnable()
    {
        onEnable.Invoke();
    }
}