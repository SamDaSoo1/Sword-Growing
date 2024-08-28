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
    string notiTextJewel = "������ �����մϴ�.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "���� �Ϸ�";

    public void Init()
    {
        // �̹� ��ٸ� �����ߴٴ� UI�� ����
        if (DataManager.Instance.GetItemUpgradeStateData(idx))
        {
            purchaseText.text = "���ſϷ�";
            costText.text = "0";
            button.interactable = false;
        }
        // �Ȼ�ٸ� �Ȼ�ٴ� UI�� ����
        else
        {
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

        purchaseText.text = "���ſϷ�";
        costText.text = "0";
        button.interactable = false;
    }
}
