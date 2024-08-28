using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine.UI;
using System.Collections;

public class ShootStar : MonoBehaviour, IDragHandler
{
    RectTransform range_of_movement;
    Vector2 savePos;
    RectTransform rect;

    bool isDrag = true;
    bool isMiss = false;

    Vector3 endValue = new Vector3(0, 0, 2160);
    float duration = 2f;

    //  아무것도 못맞추고 밖으로 나가는 궤적일 때 사라지는 범위
    int yMax = 680;
    int xMin = -480;
    int xMax = 480;

    void Start()
    {
        range_of_movement = GameObject.Find("Canvas").transform.Find("Range of movement").GetComponent<RectTransform>();
        rect = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(rect.anchoredPosition.y > yMax || rect.anchoredPosition.x < xMin || rect.anchoredPosition.x > xMax)
            Destroy(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDrag) return;

        // RectTransform의 네 모서리 좌표를 담을 배열 생성
        Vector3[] corners = new Vector3[4];
        range_of_movement.GetWorldCorners(corners);

        Vector3 topLeft = corners[1];     // 왼쪽 위
        Vector3 topRight = corners[2];    // 오른쪽 위
        Vector3 bottomLeft = corners[0];  // 왼쪽 아래
        Vector3 bottomRight = corners[3]; // 오른쪽 아래

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));

        if (worldPosition.x < topLeft.x) worldPosition.x = topLeft.x;
        if (worldPosition.x > topRight.x) worldPosition.x = topRight.x;
        if (worldPosition.y < bottomLeft.y) worldPosition.y = bottomLeft.y;
        if (worldPosition.y > topLeft.y) worldPosition.y = topLeft.y;

        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);

        if (transform.position.y < topLeft.y - 2)
        {
            isDrag = true;
            savePos = rect.anchoredPosition;
        }
        else if(transform.position.y > topLeft.y - 0.1f)
        {
            SoundManager.Instance.PlaySFX(Sfx.Throwing, 0.1f);

            isDrag = false;
            Vector2 endPos = rect.anchoredPosition;
            Vector2 dir = (endPos - savePos).normalized;

            int num = Random.Range(0, 2);
            if (num == 0)
                rect.DORotate(endValue, duration, RotateMode.FastBeyond360);
            else
                rect.DORotate(-endValue, duration, RotateMode.FastBeyond360);

            rect.DOAnchorPos(dir * 2000, duration)
                .OnComplete
                (() =>
                {
                    SceneStateMonitor.Count -= 1;
                    isMiss = true;
                });
        }
    }

    private void OnDestroy()
    {
        int swordLevel = int.Parse(gameObject.GetComponent<Image>().sprite.name);
        Vector3 collisionPos = transform.position;
        EffectPoolManager.Instance.PlayEffect(swordLevel - 1, collisionPos);

        rect.DOKill();
        if(!isMiss)
        {
            SceneStateMonitor.Count -= 1;
        }
    }
}
