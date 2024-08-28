using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    [SerializeField] RectTransform pointerImg;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] List<GameObject> blockImgList;

    public bool isClick { get; set; } = false;
    Coroutine co;

    void Start()
    {
        if (JsonFileManager<HaveStarData>.Instance.Read("HaveStarData") == default)
        {
            pointerImg.gameObject.SetActive(true);
            tmp.gameObject.SetActive(true);
            foreach (GameObject go in blockImgList)
            {
                go.SetActive(true);
            }

            ThrowingStarsMakeButton.ClickEvent += End;
            co = StartCoroutine(PointerScale());
        }
        else
        {
            pointerImg.gameObject.SetActive(false);
            tmp.gameObject.SetActive(false);
            foreach (GameObject go in blockImgList)
            {
                go.SetActive(false);
            }
        }
    }

    void End()
    {
        StopCoroutine(co);
        pointerImg.gameObject.SetActive(false);
        tmp.gameObject.SetActive(false);
        foreach (GameObject go in blockImgList)
        {
            go.SetActive(false);
        }

        ThrowingStarsMakeButton.ClickEvent -= End;
    }

    IEnumerator PointerScale()
    {
        while(true)
        {
            pointerImg.DOScale(0.8f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            pointerImg.DOScale(1.1f, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
