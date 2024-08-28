using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.UI;

public class Sort : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Slider slider;
    StarsGroup starsGroup;

    Vector2 startPosition = new Vector2(-270.0f, 400.0f);

    int count = 0;
    readonly int columnSize = 10;
    readonly int xSpacing = 60;
    readonly int ySpacing = 290;

    readonly float duration = 0.5f;

    bool isCoolTime = false;

    private void Awake()
    {
        slider.value = 1;
    }

    void Start()
    {
        starsGroup = GameObject.Find("Canvas").GetComponentInChildren<StarsGroup>();
    }
    
    IEnumerator CoolTime()
    {
        float time = 0;
        while(time < 2f)
        {
            time += Time.deltaTime;
            slider.value = time / 2;
            yield return null;
        }
        slider.value = 1;
        isCoolTime = false;
        button.interactable = true;
    }

    public void Click()
    {
        if (isCoolTime) return;

        SoundManager.Instance.PlaySFX(Sfx.Button);
        button.interactable = false;
        isCoolTime = true;
        StartCoroutine(CoolTime());
        List<RectTransform> starList = starsGroup.GetChild();
        Vector2 starPosition = startPosition;
        foreach (RectTransform star in starList)
        {
            if (star.GetComponent<DoTweenController>().IsTweening) continue;

            star.DOAnchorPos(starPosition, duration);
            count++;
            starPosition.x += xSpacing;
            if(count % columnSize == 0)
            {
                starPosition.x -= xSpacing * 10;
                starPosition.y -= ySpacing;
            }
        }
        count = 0;
    }

    public void Init()
    {
        List<RectTransform> starList = starsGroup.GetChild();
        Vector2 starPosition = startPosition;
        foreach (RectTransform star in starList)
        {
            star.anchoredPosition = starPosition;
            count++;
            starPosition.x += xSpacing;
            if (count % columnSize == 0)
            {
                starPosition.x -= xSpacing * 10;
                starPosition.y -= ySpacing;
            }
        }
        count = 0;
    }
}
