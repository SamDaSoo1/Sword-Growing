using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInitialize : MonoBehaviour
{
    [SerializeField] List<UpgradeBoxUI> upgradeUI;

    [SerializeField] ItemBoxUI1 itemBoxUI1;
    [SerializeField] ItemBoxUI2 itemBoxUI2;
    [SerializeField] ItemBoxUI3 itemBoxUI3;
    [SerializeField] ItemBoxUI4 itemBoxUI4;
    [SerializeField] ItemBoxUI5 itemBoxUI5;
    [SerializeField] ItemBoxUI6 itemBoxUI6;
    [SerializeField] RectTransform resolution;
    [SerializeField] RectTransform[] canvasArray;
    [SerializeField] RectTransform[] uiArray;
    [SerializeField] RectTransform rangeOfMovement;
    [SerializeField] RectTransform tabBgd;
    [SerializeField] RectTransform starTabScrollView;
    [SerializeField] RectTransform upgradeTabScrollView;
    [SerializeField] RectTransform itemScrollView;
    [SerializeField] RectTransform canvasSize;

    void Start()
    {
        Init();
        AdjustUIResolution();
    }

    void Init()
    {
        for (int i = 0; i < upgradeUI.Count; i++)
        {
            upgradeUI[i].Init();
        }
        itemBoxUI1.Init();
        itemBoxUI2.Init();
        itemBoxUI3.Init();
        itemBoxUI4.Init();
        itemBoxUI5.Init();
        itemBoxUI6.Init();
    }

    void AdjustUIResolution()
    {
        float h = resolution.rect.height;
        float h2 = Screen.safeArea.height;
        float h3 = canvasSize.rect.height;
        float result = h - h2;
        //print($"-----------------------화면 크기: {h}, 안전지대: {h2}, 캔버스 크기: {h3}-------------------------");
        rangeOfMovement.sizeDelta = new Vector2(rangeOfMovement.gameObject.transform.parent.GetComponentInParent<RectTransform>().rect.width * 0.65f,
                                                rangeOfMovement.gameObject.transform.parent.GetComponentInParent<RectTransform>().rect.height * 0.65f);

        foreach(RectTransform ui in uiArray)
        {
            ui.anchoredPosition = new Vector2(ui.anchoredPosition.x, ui.anchoredPosition.y - result);
        }

        rangeOfMovement.anchoredPosition = new Vector2(rangeOfMovement.anchoredPosition.x, rangeOfMovement.anchoredPosition.y - result);
        tabBgd.anchoredPosition = new Vector2(tabBgd.anchoredPosition.x, tabBgd.anchoredPosition.y - result);

        float posY = -1150;
        float newPosY = posY - ((canvasSize.rect.height - 1920) / 2);
        float newHeight = (canvasSize.rect.height + newPosY) * 2 - 40 - result;
        //print($"newPosY: {newPosY}, newHeight: {newHeight}");
        starTabScrollView.anchoredPosition = new Vector2(starTabScrollView.anchoredPosition.x, newPosY - (result / 2));
        upgradeTabScrollView.anchoredPosition = new Vector2(upgradeTabScrollView.anchoredPosition.x, newPosY - (result / 2));
        itemScrollView.anchoredPosition = new Vector2(itemScrollView.anchoredPosition.x, newPosY - (result / 2));

        starTabScrollView.sizeDelta = new Vector2(starTabScrollView.sizeDelta.x, newHeight);
        upgradeTabScrollView.sizeDelta = new Vector2(upgradeTabScrollView.sizeDelta.x, newHeight);
        itemScrollView.sizeDelta = new Vector2(itemScrollView.sizeDelta.x, newHeight);
    }
}
