using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Magnet : MonoBehaviour
{
    [SerializeField] Slider coolTimeBar;
    [SerializeField] GameObject disableImg;
    [SerializeField] Button button;
    [SerializeField] Image img;
    [SerializeField] StarsGroup starsGroup;

    Color deactivateColor = new Color(1, 1, 1, 0.5f);

    int coolTime = 3;

    Coroutine co;

    public bool ActiveSelf { get; private set; } = true;

    private void Awake()
    {
        coolTimeBar.value = 0;
        disableImg.SetActive(false);
        button.interactable = false;
        coolTimeBar.gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        button.interactable = true;
        coolTimeBar.gameObject.SetActive(true);
        img.color = Color.white;
        co = StartCoroutine(MagnetCoroutine());
    }

    public void Deactivate()
    {
        button.interactable = false;
        coolTimeBar.gameObject.SetActive(false);

        ColorBlock colorBlock = button.colors;
        colorBlock.disabledColor = deactivateColor;
        button.colors = colorBlock;
        img.color = deactivateColor;
        gameObject.SetActive(false);
    }

    IEnumerator MagnetCoroutine()
    {
        float time = 0f;
        while (time < coolTime)
        {
            time += Time.deltaTime;
            coolTimeBar.value = time / 3;
            yield return null;
        }

        coolTimeBar.value = 1f;
        StartCoroutine(MagnetOperation());       
        co = StartCoroutine(MagnetCoroutine());
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        // disableImg 활성화 하고, 슬라이더 끄고
        if (!disableImg.activeSelf)
        {
            ActiveSelf = false;
            disableImg.SetActive(true);
            coolTimeBar.gameObject.SetActive(false);
            StopCoroutine(co);
        }
        else
        {
            ActiveSelf = true;
            disableImg.SetActive(false);
            coolTimeBar.gameObject.SetActive(true);
            co = StartCoroutine(MagnetCoroutine());
        }
    }

    readonly int xMinimum = -300;
    readonly int xMax = 371;
    readonly int yMinimum = -350;
    readonly int yMax = 301;
    readonly float duration = 0.9f;

    IEnumerator MagnetOperation()
    {
        List<GameObject> list = starsGroup.PickTwoObj();

        if (list.Count < 2)
        {
            yield break;
        }    

        Vector2 ranDomPos = new Vector2(Random.Range(xMinimum, xMax), Random.Range(yMinimum, yMax));

        list[0].GetComponent<DoTweenController>().IsTweening = true;
        list[1].GetComponent<DoTweenController>().IsTweening = true;

        list[0].transform.GetComponent<Image>().raycastTarget = false;
        list[1].transform.GetComponent<Image>().raycastTarget = false;

        list[0].transform.GetComponent<RectTransform>().DOAnchorPos(ranDomPos, duration).SetEase(Ease.OutCubic);
        list[1].transform.GetComponent<RectTransform>().DOAnchorPos(ranDomPos, duration).SetEase(Ease.OutCubic);

        yield return new WaitForSeconds(duration + 0.05f);
        list[1].GetComponent<CombineThrowingStar>().CombineCheck();
    }
}
