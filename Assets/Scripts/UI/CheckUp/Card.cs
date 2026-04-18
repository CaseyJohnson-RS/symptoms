using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [Header("Hover Settings"), Space(5)]
    public float hoverOffsetY = 80f;
    public float hoverScale = 1.2f;
    public float hoverRotationZ = 0f;

    [Header("Animation"), Space(5)]
    public float positionSpeed = 10f;
    public float scaleSpeed = 10f;
    public float rotationSpeed = 10f;

    [Header("Behavior"), Space(5)]
    public bool bringToFrontOnHover = true; // ← новая галочка

    [Header("Events"), Space(5)]
    public UnityEvent onHower;

    private Vector3 basePosition;
    private Vector3 targetPosition;

    private Vector3 baseScale;
    private Vector3 targetScale;

    private Quaternion baseRotation;
    private Quaternion targetRotation;

    private bool isHovered = false;

    void Start()
    {
        basePosition = transform.localPosition;
        targetPosition = basePosition;

        baseScale = transform.localScale;
        targetScale = baseScale;

        baseRotation = transform.localRotation;
        targetRotation = baseRotation;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * positionSpeed
        );

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * scaleSpeed
        );

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;

        targetPosition = basePosition + Vector3.up * hoverOffsetY;
        targetScale = baseScale * hoverScale;
        targetRotation = Quaternion.Euler(0, 0, hoverRotationZ);

        if (bringToFrontOnHover)
        {
            transform.SetAsLastSibling();
        }

        onHower.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;

        targetPosition = basePosition;
        targetScale = baseScale;
        targetRotation = baseRotation;
    }

    public void SetBaseTransform(Vector3 pos, Quaternion rot)
    {
        basePosition = pos;
        baseRotation = rot;

        if (!isHovered)
        {
            targetPosition = pos;
            targetRotation = rot;
        }
    }
}