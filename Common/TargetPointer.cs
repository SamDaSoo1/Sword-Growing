using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{
    RectTransform rect;

    [SerializeField] float speed = 1000f;  // ���� ������ �ӵ� �󸶳� �������� �� �׽�Ʈ������ �ν����ͷ� ������
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
