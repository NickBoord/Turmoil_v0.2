using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyTarget : MonoBehaviour
{
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public bool IsArrowOver(Vector3 arrowPos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, arrowPos, null);
    }

    public Vector3 GetLockOnPoint()
    {
        return rectTransform.position;
    }

    
}
