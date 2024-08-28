using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button button;

    int idx = 0;
    int gold = 1000;

    Color notiColorGold = new Color(1, 158 / 255f, 60 / 255f);
    string notiTextGold = "골드가 부족합니다.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "구매 완료";

    public void Init()
    {
        // 이미 샀다면 구매했다는 UI로 변경
        if(DataManager.Instance.GetItemUpgradeStateData(idx))
        {
            purchaseText.text = "구매완료";
            costText.text = "0";
            button.interactable = false;
        }
        // 안샀다면 안샀다는 UI로 변경
        else
        {
            purchaseText.text = "구매하기";
            costText.text = $"{gold}";
            button.interactable = true;
        }
    }

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= gold)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            Upgrade();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    void Upgrade()
    {
        UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);

        // 데이터 갱신하고
        DataManager.Instance.Gold -= gold;              // 골드 데이터 갱신
        DataManager.Instance.Star_Make_CoolTime -= 1;   // 검 제작 시간 데이터 갱신
        DataManager.Instance.SetItemUpgradeStateData(idx);

        // UI 갱신하고
        purchaseText.text = "구매완료";
        costText.text = "0";
        button.interactable = false;
    }
}
