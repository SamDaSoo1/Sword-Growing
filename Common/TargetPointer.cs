using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    RectTransform rect;

    [SerializeField] float speed = 1000f;  // 맞출 때마다 속도 얼마나 빠르게할 지 테스트용으로 인스펙터로 빼놓음
    float maxX = 800.0f;
    float minX = -800.0f;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-500.0f, rect.anchoredPosition.y);
    }

    private void Update()
    {
        MoveImage();
        CheckBoundary();
    }

    void MoveImage()
    {
        rect.anchoredPosition += new Vector2(Time.deltaTime * speed, 0);
    }

    void CheckBoundary()
    {
        if (rect.anchoredPosition.x > maxX)
        {
            rect.anchoredPosition = new Vector2(minX, rect.anchoredPosition.y);
        }
    }

    public void SpeedUp()
    {
        speed += 200;
    }
}
