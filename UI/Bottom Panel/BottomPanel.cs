using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPanel : MonoBehaviour
{
    TabBgd tabBgd;
    StarTab starTab;
    UpgradeTab upgradeTab;
    ItemTab itemTab;

    StarTabScrollView starTabScrollView;
    UpgradeScrollView upgradeScrollView;
    ItemTabScrollView itemTabScrollView;

    void Start()
    {
        tabBgd = GameObject.Find("Canvas").transform.Find("Tab Bgd").GetComponent<TabBgd>();
        starTab = GameObject.Find("Canvas").transform.Find("Bottom Panel").transform.Find("StarTab").GetComponent<StarTab>();
        upgradeTab = GameObject.Find("Canvas").transform.Find("Bottom Panel").transform.Find("UpgradeTab").GetComponent<UpgradeTab>();
        itemTab = GameObject.Find("Canvas").transform.Find("Bottom Panel").transform.Find("ItemTab").GetComponent<ItemTab>();

        starTabScrollView = GameObject.Find("Canvas").transform.Find("StarTab Scroll View").GetComponent<StarTabScrollView>();
        upgradeScrollView = GameObject.Find("Canvas").transform.Find("Upgrade Scroll View").GetComponent<UpgradeScrollView>();
        itemTabScrollView = GameObject.Find("Canvas").transform.Find("Item Scroll View").GetComponent<ItemTabScrollView>();
    }

    public void StarTabClick(bool isPressed)
    {

        if (!isPressed)
        {
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
            tabBgd.Off();
            upgradeScrollView.Off();
        }
    }

    public void ItemTabClick(bool isPressed)
    {
        if (!isPressed)
        {
            itemTab.Pressed();
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
            tabBgd.Off();
            itemTabScrollView.Off();
        }
    }
}
