using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoTweenController : MonoBehaviour
{
    public bool IsTweening { get; set; } = false;

    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    
    public void Rotation(int xMin, int xMax, int yMin, int yMax, float duration, Vector3 endValue)
    {
        Vector2 ranDomPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        rect.DOAnchorPos(ranDomPos, duration);
        rect.DORotate(endValue, duration, RotateMode.FastBeyond360);
    }
}
