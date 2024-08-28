using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CombineThrowingStar : MonoBehaviour, IDragHandler, IEndDragHandler
{
    ThrowingStarsMake throwingStarsMake;
    GameObject StarsGroup;
    RectTransform range_of_movement;

    int numbering;         // 할당되는 값이 수비 표창 == 1, 무한의 수리검 == 16
    int lastStar = 16;

    float radius = 30.0f;

    string wordToRemove = "(Clone)";

    void Awake()
    {
        numbering = int.Parse(GetComponent<Image>().sprite.name);
    }

    void Start()
    {
        throwingStarsMake = GameObject.Find("Canvas").GetComponentInChildren<ThrowingStarsMake>();
        StarsGroup = GameObject.Find("Canvas").transform.Find("StarsGroup").gameObject;
        range_of_movement = GameObject.Find("Canvas").transform.Find("Range of movement").GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject.GetComponent<DoTweenController>().IsTweening)
            return;

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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 엔드표창끼리는 합체 못하게 막아놓음
        if (numbering == lastStar || gameObject.GetComponent<DoTweenController>().IsTweening)
            return;

        CombineCheck();
    }

    public void CombineCheck()
    {
        GameObject NearestStar = null;
        Collider2D[] colliders;
        float curDist = float.MaxValue;
        float dist;
        string grade;

        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            grade = collider.name;
            if (collider.gameObject != gameObject && grade == gameObject.name)
            {
                dist = Vector2.Distance(collider.gameObject.transform.position, transform.position);
                if (curDist > dist)
                {
                    curDist = dist;
                    NearestStar = collider.gameObject;
                }
            }
        }

        if (NearestStar != null)
        {
            Combine(NearestStar);
        }
    }

    void Combine(GameObject NearestStar)
    {
        SoundManager.Instance.PlaySFX(Sfx.SwordCreate, 0.15f);
        Vector3 newStarPos = transform.position;
        Destroy(NearestStar);
        Destroy(transform.gameObject);
        GameObject newStar = Instantiate(throwingStarsMake.GetStar(numbering), StarsGroup.transform);
        if(newStar.GetComponent<ThrowingStarData>() != null)
        {
            newStar.GetComponent<ThrowingStarData>().enabled = false;
        }

        newStar.name = newStar.name.Replace(wordToRemove, "");
        newStar.transform.position = newStarPos;
        DataManager.Instance.Combine(numbering);
        UIDisplay.Instance.GetJewelUI(numbering, newStarPos);
        EffectPoolManager.Instance.PlayEffect(numbering - 1, newStarPos);
        DataManager.Instance.TotalCount -= 1;
    }

    public void LevelUp()
    {
        SoundManager.Instance.PlaySFX(Sfx.SwordCreate, 0.15f);
        Vector3 newStarPos = transform.position;
        Destroy(transform.gameObject);
        GameObject newStar = Instantiate(throwingStarsMake.GetStar(numbering), StarsGroup.transform);
        if (newStar.GetComponent<ThrowingStarData>() != null)
        {
            newStar.GetComponent<ThrowingStarData>().enabled = false;
        }

        newStar.name = newStar.name.Replace(wordToRemove, "");
        newStar.transform.position = newStarPos;
        DataManager.Instance.LevelUp(numbering);
        UIDisplay.Instance.GetJewelUI(numbering, newStarPos);
        EffectPoolManager.Instance.PlayEffect(numbering - 1, newStarPos);
    }

    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
#endif
    }
}
