using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI5 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lvText;
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button button;

    int level = 0;
    int idx = 1;
    int jewel = 100;

    Color notiColorJewel = new Color(128 / 255f, 117 / 255f, 224 / 255f);
    string notiTextJewel = "보석이 부족합니다.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "구매 완료";

    public void Init()
    {
        level = DataManager.Instance.GetItemUpgradeLevelData(idx);
        jewel = 100 * (level + 1);

        if (level == 5)
        {
            button.interactable = false;
            lvText.text = "Lv MAX";
            purchaseText.text = "구매완료";
            costText.text = "0";
        }
        else if (level < 5)
        {
            lvText.text = $"Lv {level}";
            purchaseText.text = "구매하기";
            costText.text = $"{jewel}";
        }
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= jewel)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            LevelUp();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    void LevelUp()
    {
        UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);

        level++;
        DataManager.Instance.Jewel -= jewel;
        jewel = 100 * (level + 1);
        if (level == 5)
        {
            DataManager.Instance.Maximum_Star_Count += 1;
            DataManager.Instance.AddItemUpgradeLevelData(idx);
            lvText.text = "Lv MAX";
            purchaseText.text = "구매완료";
            costText.text = "0";
            button.interactable = false;
        }
        else if (level < 5)
        {
            DataManager.Instance.Maximum_Star_Count += 1;
            DataManager.Instance.AddItemUpgradeLevelData(idx);
            lvText.text = $"Lv {level}";
            purchaseText.text = "구매하기";
            costText.text = $"{jewel}";
        }
    }
}
