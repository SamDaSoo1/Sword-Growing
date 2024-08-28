using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI1 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lvText;
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button button;

    int level = 0;
    int levelMax = 5;
    int idx = 0;
    int gold = 1000;

    Color notiColorGold = new Color(1, 158 / 255f, 60 / 255f);
    string notiTextGold = "골드가 부족합니다.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "구매 완료";

    public void Init()
    {
        level = DataManager.Instance.GetItemUpgradeLevelData(idx);
        gold = 1000 * (level + 1);

        if (level == levelMax)
        {
            button.interactable = false;
            lvText.text = "Lv MAX";
            purchaseText.text = "구매완료";
            costText.text = "0";
        }
        else if (level < levelMax)
        {
            lvText.text = $"Lv {level}";
            purchaseText.text = "구매하기";
            costText.text = $"{gold}";
        }
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= gold)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            LevelUp();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    void LevelUp()
    {
        UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
        level++;
        DataManager.Instance.Gold -= gold; 
        gold = 1000 * (level + 1);
        if (level == levelMax)
        {
            DataManager.Instance.Maximum_Star_Count += 1;
            DataManager.Instance.AddItemUpgradeLevelData(idx);
            lvText.text = "Lv MAX";
            purchaseText.text = "구매완료";
            costText.text = "0";
            button.interactable = false;
        }
        else if (level < levelMax)
        {
            DataManager.Instance.Maximum_Star_Count += 1;
            DataManager.Instance.AddItemUpgradeLevelData(idx);
            lvText.text = $"Lv {level}";
            purchaseText.text = "구매하기";
            costText.text = $"{gold}";
        }
    }
}
