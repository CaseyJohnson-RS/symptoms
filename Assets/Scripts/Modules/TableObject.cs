using UnityEngine;
using System;

public class TableObject : MonoBehaviour
{

    [SerializeField] private RectTransform innerShadow;
    [SerializeField] private RectTransform outerShadow;

    
    // public Vector2 lightPosition = new Vector2(0.5, 0.2)

    private bool drag = false;

    private void OnMouseDown()
    {
        drag = true;
    }

    private void OnMouseUp()
    {
        drag = false;
    }

    private void Update()
    {
        if (drag)
        {
            
        }
    }
}
