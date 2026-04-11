using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [Header("Настройки качания rotation")]
    [SerializeField] private float amplitude = 50f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private int axisIndex = 0;

    private Vector3 startRotation;

    void Start()
    {
        startRotation = transform.localEulerAngles;
    }

    void Update()
    {
        float time = Time.time;
        float offset = Mathf.Sin(time * frequency * 2 * Mathf.PI) * amplitude;
        Vector3 newRotation = startRotation;
        newRotation[axisIndex] = startRotation[axisIndex] + offset;  // Добавляем к стартовой rotation
        transform.localEulerAngles = newRotation;
    }
}