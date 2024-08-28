using UnityEngine;

public class LightEffect : MonoBehaviour
{
    [SerializeField] RectTransform rect;

    float angle = 0f;
    float speed = 50f;

    void Update()
    {
        angle += Time.deltaTime * speed;
        rect.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
