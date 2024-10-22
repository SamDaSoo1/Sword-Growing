using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    [SerializeField] GameObject supportBgd;
    [SerializeField] TabBgd tabBgd;
    [SerializeField] StarTab starTab;
    [SerializeField] UpgradeTab upgradeTab;
    [SerializeField] ItemTab itemTab;
    [SerializeField] StarTabScrollView starTabScrollView;
    [SerializeField] UpgradeScrollView upgradeScrollView;
    [SerializeField] ItemTabScrollView itemTabScrollView;

    void Start()
    {
        supportBgd.SetActive(false);
    }

    public void StarTabClick(bool isPressed)
    {
        if (!isPressed)
        {
            supportBgd.SetActive(true);
            starTab.Pressed();
            tabBgd.On();
            starTabScrollView.On();

            upgradeTab.Normal();
            upgradeScrollView.Off();
            itemTab.Normal();
            itemTabScrollView.Off();
        }
        else
        {
            supportBgd.SetActive(false);
            starTab.Normal();
            tabBgd.Off();
            starTabScrollView.Off();
        }
    }

    public void UpgradeTabClick(bool isPressed)
    {
        if (!isPressed)
        {
            upgradeTab.Pressed();
            supportBgd.SetActive(true);
            tabBgd.On();
            upgradeScrollView.On();

            starTab.Normal();
            starTabScrollView.Off();
            itemTab.Normal();
            itemTabScrollView.Off();
        }
        else
        {
            upgradeTab.Normal();
            supportBgd.SetActive(false);
            tabBgd.Off();
            upgradeScrollView.Off();
        }
    }

    public void ItemTabClick(bool isPressed)
    {
        if (!isPressed)
        {
            itemTab.Pressed();
            supportBgd.SetActive(true);
            tabBgd.On();
            itemTabScrollView.On();

            starTab.Normal();
            starTabScrollView.Off();
            upgradeTab.Normal();
            upgradeScrollView.Off();
        }
        else
        {
            itemTab.Normal();
            supportBgd.SetActive(false);
            tabBgd.Off();
            itemTabScrollView.Off();
        }
    }
}
