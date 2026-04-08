using UnityEngine;

public class ObjectShadow : MonoBehaviour
{
    [SerializeField] private RectTransform innerShadow;
    [SerializeField] private RectTransform outerShadow;
    [SerializeField] private RectTransform lightSource;
    [SerializeField] private float outerShadowOffset = 20f;
    [SerializeField] private bool activeOnStart;

    private Vector2 outerShadowOrigin;

    private void Start()
    {
        if (outerShadow)
            outerShadowOrigin = outerShadow.anchoredPosition;

        if (!lightSource)
        {
            GameObject light = GameObject.FindWithTag("LightPoint");
            if (light)
                lightSource = light.GetComponent<RectTransform>();
        }

        enabled = activeOnStart;
        UpdateShadows();
    }

    private void Update()
    {
        UpdateShadows();
    }

    private void UpdateShadows()
    {
        if (!lightSource) return;

        if (innerShadow)
        {
            Vector3 directionToLight = lightSource.position - innerShadow.position;
            float angle = Mathf.Atan2(directionToLight.y, directionToLight.x) * Mathf.Rad2Deg - 90f;
            innerShadow.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (outerShadow)
        {
            Vector3 directionFromLight = (transform.position - lightSource.position).normalized;
            float horizontalShift = directionFromLight.x * outerShadowOffset;
            outerShadow.anchoredPosition = outerShadowOrigin + new Vector2(horizontalShift, 0f);
        }
    }
}