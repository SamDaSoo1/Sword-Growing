using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI3 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI purchaseText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] Button button;
    [SerializeField] Magnet magnet;

    int jewel = 200;

    Color notiColorJewel = new Color(128 / 255f, 117 / 255f, 224 / 255f);
    string notiTextJewel = "보석이 부족합니다.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "구매 완료";

    public void Init()
    {
        if (DataManager.Instance.Magnet)
        {
            magnet.Activate();
            purchaseText.text = "구매완료";
            costText.text = "0";
            button.interactable = false;
        }
        else
        {
            magnet.Deactivate();
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
            Purchase();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    void Purchase()
    {
        UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);

        // 데이터 갱신하고
        DataManager.Instance.Jewel -= jewel;
        DataManager.Instance.Magnet = true;
        magnet.Activate();

        purchaseText.text = "구매완료";
        costText.text = "0";
        button.interactable = false;
    }
}
