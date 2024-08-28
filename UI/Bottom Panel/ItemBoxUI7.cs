using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxUI7 : MonoBehaviour
{
    [SerializeField] GameObject getNewWeaponPopup;
    [SerializeField] StarsGroup starsGroup;
    [SerializeField] Magnet magnet;
    [SerializeField] AutoMaking autoMaking;

    int gold = 50000;

    Color notiColorGold = new Color(1, 158 / 255f, 60 / 255f);
    string notiTextGold = "��尡 �����մϴ�.";
    Color notiColorLiberationFailed = new Color(126 / 255f, 23 / 255f, 63 / 255f);
    string notiTextLiberationFailed = "��� ���� �ִ� �����Դϴ�.";
    Color notiColorMagnetOffPlz = new Color(40 / 255f, 144 / 255f, 133 / 255f);
    string notiTextMagnetOffPlz = "�ڼ��� ���ּ���.";

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= gold)
        {
            Liberation();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    void Liberation()
    {
        GameObject pick = starsGroup.PickOneObj();
        if(pick == null)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorLiberationFailed, notiTextLiberationFailed);
            return;
        }

        if(magnet.gameObject.activeSelf && magnet.ActiveSelf)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorMagnetOffPlz, notiTextMagnetOffPlz);
            return;
        }

        SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);

        // ������ �����ϰ�
        DataManager.Instance.Gold -= gold;              // ��� ������ ����
        pick.GetComponent<CombineThrowingStar>().LevelUp();
        UIDisplay.Instance.GetNewWeaponPopup(int.Parse(pick.name) + 1);
    }
}
