using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.IO;
using System;

public class ThrowingStarsMake : MonoBehaviour
{
    [SerializeField]
    List<GameObject> stars;

    [SerializeField]
    GameObject StarsGroup;

    RectTransform starRect;

    readonly int xMinimum = -300;
    readonly int xMax = 371;
    readonly int yMinimum = -350;
    readonly int yMax = 301;

    readonly float duration = 0.5f;

    string wordToRemove = "(Clone)";

    Vector3 endValue = new Vector3(0, 0, 1080);
    
    public void Make()
    {
        GameObject star = Instantiate(stars[0], StarsGroup.transform);
        if (star.GetComponent<ThrowingStarData>() != null)
        {
            star.GetComponent<ThrowingStarData>().enabled = false;
        }

        star.name = star.name.Replace(wordToRemove, "");
        starRect = star.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").GetComponentInParent<RectTransform>(), transform.position, null, out Vector2 localPosition);
        starRect.anchoredPosition = localPosition;

        if (DataManager.Instance != null)
            DataManager.Instance.UpdateStarDataList();

        star.GetComponent<DoTweenController>().Rotation(xMinimum, xMax, yMinimum, yMax, duration, endValue);
    }

    public GameObject GetStar(int index)
    {
        return stars[index];
    }

    void OnDestroy()
    {
        ThrowingStarsMakeButton.ClickEvent -= Make;
    }

    public void Init()
    {
        stars = new List<GameObject>()
        {
            Resources.Load<GameObject>("Prefabs/0"),
            Resources.Load<GameObject>("Prefabs/1"),
            Resources.Load<GameObject>("Prefabs/2"),
            Resources.Load<GameObject>("Prefabs/3"),
            Resources.Load<GameObject>("Prefabs/4"),
            Resources.Load<GameObject>("Prefabs/5"),
            Resources.Load<GameObject>("Prefabs/6"),
            Resources.Load<GameObject>("Prefabs/7"),
            Resources.Load<GameObject>("Prefabs/8"),
            Resources.Load<GameObject>("Prefabs/9"),
            Resources.Load<GameObject>("Prefabs/10"),
            Resources.Load<GameObject>("Prefabs/11"),
            Resources.Load<GameObject>("Prefabs/12"),
            Resources.Load<GameObject>("Prefabs/13"),
            Resources.Load<GameObject>("Prefabs/14"),
            Resources.Load<GameObject>("Prefabs/15")
        };

        ThrowingStarsMakeButton.ClickEvent += Make;
        StarsGroup = GameObject.Find("Canvas").transform.Find("StarsGroup").gameObject;

        int idx = 0;
        HaveStarData haveStarData;

        haveStarData = JsonFileManager<HaveStarData>.Instance.Read("HaveStarData");

        if (haveStarData == default)
            return;

        foreach (int value in haveStarData.starsCount)
        {
            if(value == 0)
            {
                idx++;
                continue;
            }

            for (int i = 0; i < value; i++)
            {
                GameObject star = Instantiate(stars[idx], StarsGroup.transform);

                if (star.GetComponent<ThrowingStarData>() != null)
                {
                    star.GetComponent<ThrowingStarData>().enabled = false;
                }

                star.name = star.name.Replace(wordToRemove, "");
                starRect = star.GetComponent<RectTransform>();

                RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").GetComponentInParent<RectTransform>(), transform.position, null, out Vector2 localPosition);
                starRect.anchoredPosition = localPosition;

                Vector2 ranDomPos = new Vector2(UnityEngine.Random.Range(xMinimum, xMax), UnityEngine.Random.Range(yMinimum, yMax));

                starRect.anchoredPosition = ranDomPos;
            }

            idx++;
        }
    }
}
