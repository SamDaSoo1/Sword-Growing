using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitialize : MonoBehaviour
{
    [SerializeField] List<UpgradeBoxUI> upgradeUI;

    [SerializeField] ItemBoxUI1 itemBoxUI1;
    [SerializeField] ItemBoxUI2 itemBoxUI2;
    [SerializeField] ItemBoxUI3 itemBoxUI3;
    [SerializeField] ItemBoxUI4 itemBoxUI4;
    [SerializeField] ItemBoxUI5 itemBoxUI5;
    [SerializeField] ItemBoxUI6 itemBoxUI6;

    void Start()
    {
        Init();
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
}
