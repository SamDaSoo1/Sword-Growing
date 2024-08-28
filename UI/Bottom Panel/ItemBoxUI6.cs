using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI6 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button button;

    int idx = 1;
    int jewel = 200;

    Color notiColorJewel = new Color(128 / 255f, 117 / 255f, 224 / 255f);
    string notiTextJewel = "보석이 부족합니다.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "구매 완료";

    public void Init()
    {
        // 이미 샀다면 구매했다는 UI로 변경
        if (DataManager.Instance.GetItemUpgradeStateData(idx))
        {
            purchaseText.text = "구매완료";
            costText.text = "0";
            button.interactable = false;
        }
        // 안샀다면 안샀다는 UI로 변경
        else
        {
            purchaseText.text = "구매하기";
            costText.text = $"{jewel}";
            button.interactable = true;
        }
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= jewel)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            Upgrade();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    void Upgrade()
    {
        UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);

        DataManager.Instance.Jewel -= jewel;           
        DataManager.Instance.Star_Make_CoolTime -= 1;    
        DataManager.Instance.SetItemUpgradeStateData(idx);

        purchaseText.text = "구매완료";
        costText.text = "0";
        button.interactable = false;
    }
}
