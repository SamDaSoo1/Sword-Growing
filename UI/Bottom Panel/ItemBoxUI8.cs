using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxUI8 : MonoBehaviour
{
    [SerializeField] GameObject getNewWeaponPopup;
    [SerializeField] StarsGroup starsGroup;
    [SerializeField] Magnet magnet;
    [SerializeField] AutoMaking autoMaking;

    int jewel = 500;

    Color notiColorJewel = new Color(128 / 255f, 117 / 255f, 224 / 255f);
    string notiTextJewel = "������ �����մϴ�.";
    Color notiColorLiberationFailed = new Color(126 / 255f, 23 / 255f, 63 / 255f);
    string notiTextLiberationFailed = "��� ���� �ִ� �����Դϴ�.";
    Color notiColorMagnetOffPlz = new Color(40 / 255f, 144 / 255f, 133 / 255f);
    string notiTextMagnetOffPlz = "�ڼ��� ���ּ���.";

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= jewel)
        {
            Liberation();
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    void Liberation()
    {
        GameObject pick = starsGroup.PickOneObj();
        if (pick == null)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorLiberationFailed, notiTextLiberationFailed);
            return;
        }

        if (magnet.gameObject.activeSelf && magnet.ActiveSelf)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorMagnetOffPlz, notiTextMagnetOffPlz);
            return;
        }

        SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);

        // ������ �����ϰ�
        DataManager.Instance.Jewel -= jewel;
        pick.GetComponent<CombineThrowingStar>().LevelUp();
        UIDisplay.Instance.GetNewWeaponPopup(int.Parse(pick.name) + 1);
    }
}
