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
    string notiTextJewel = "������ �����մϴ�.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "���� �Ϸ�";

    public void Init()
    {
        if (DataManager.Instance.Magnet)
        {
            magnet.Activate();
            purchaseText.text = "���ſϷ�";
            costText.text = "0";
            button.interactable = false;
        }
        else
        {
            magnet.Deactivate();
            purchaseText.text = "�����ϱ�";
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

        // ������ �����ϰ�
        DataManager.Instance.Jewel -= jewel;
        DataManager.Instance.Magnet = true;
        magnet.Activate();

        purchaseText.text = "���ſϷ�";
        costText.text = "0";
        button.interactable = false;
    }
}
