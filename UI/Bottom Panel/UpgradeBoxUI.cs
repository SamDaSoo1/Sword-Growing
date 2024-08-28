using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBoxUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] int idx;
    [SerializeField] Button button;

    public bool IsPurchase { get; private set; } = false;

    public void Init()
    {
        if (DataManager.Instance.GetUpgradeState(idx))
            UIUpdate();
    }

    public void UIUpdate()
    {
        purchaseText.text = "���ſϷ�";
        costText.text = 0.ToString();
        IsPurchase = true;
        button.interactable = false;
    }
}
