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
    string notiTextGold = "��尡 �����մϴ�.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "���� �Ϸ�";

    public void Init()
    {
        // �̹� ��ٸ� �����ߴٴ� UI�� ����
        if(DataManager.Instance.GetItemUpgradeStateData(idx))
        {
            purchaseText.text = "���ſϷ�";
            costText.text = "0";
            button.interactable = false;
        }
        // �Ȼ�ٸ� �Ȼ�ٴ� UI�� ����
        else
        {
            purchaseText.text = "�����ϱ�";
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

        // ������ �����ϰ�
        DataManager.Instance.Gold -= gold;              // ��� ������ ����
        DataManager.Instance.Star_Make_CoolTime -= 1;   // �� ���� �ð� ������ ����
        DataManager.Instance.SetItemUpgradeStateData(idx);

        // UI �����ϰ�
        purchaseText.text = "���ſϷ�";
        costText.text = "0";
        button.interactable = false;
    }
}
