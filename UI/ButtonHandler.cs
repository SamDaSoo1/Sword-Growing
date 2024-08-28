using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] List<StarUpgradeBoxUI> StarUpgradeBoxUIList;
    [SerializeField] List<Button> buttons;
    [SerializeField] List<UpgradeBoxUI> UpgradeBoxUIList;
    [SerializeField] GameObject getNewWeaponPopup;

    Color notiColorGold = new Color(1, 158 / 255f, 60 / 255f);
    string notiTextGold = "골드가 부족합니다.";
    Color notiColorJewel = new Color(128 / 255f, 117 / 255f, 224 / 255f);
    string notiTextJewel = "보석이 부족합니다.";
    Color notiColorPurchased = new Color(40 / 255f, 190 / 255f, 37 / 255f);
    string notiTextPurchased = "구매 완료";

    private void Start()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            int idx = i;
            buttons[i].onClick.AddListener(() => Tab1Click(idx));
        }
    }

    public void Tab1Click(int idx)
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        StarUpgradeBoxUIList[idx].Set(idx + 1);
    }

    public void Tab2Button1()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= 100 && !UpgradeBoxUIList[0].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[0].UIUpdate();
            DataManager.Instance.Jewel -= 100;
            DataManager.Instance.Increased_Attack_Damage += 2;
            DataManager.Instance.UpgradeComplete(0);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    public void Tab2Button2()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= 1000 && !UpgradeBoxUIList[1].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[1].UIUpdate();
            DataManager.Instance.Gold -= 1000;
            DataManager.Instance.Increased_Attack_Damage += 1;
            DataManager.Instance.UpgradeComplete(1);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    public void Tab2Button3()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= 1000 && !UpgradeBoxUIList[2].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[2].UIUpdate();
            DataManager.Instance.Gold -= 1000;
            DataManager.Instance.Critical_Chance += 30;
            DataManager.Instance.UpgradeComplete(2);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    public void Tab2Button4()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= 1000 && !UpgradeBoxUIList[3].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[3].UIUpdate();
            DataManager.Instance.Gold -= 1000;
            DataManager.Instance.Critical_Damage += 1;
            DataManager.Instance.UpgradeComplete(3);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    public void Tab2Button5()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Gold >= 1000 && !UpgradeBoxUIList[4].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[4].UIUpdate();
            DataManager.Instance.Gold -= 1000;
            DataManager.Instance.Gold_Gained += 1;
            DataManager.Instance.UpgradeComplete(4);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
        }
    }

    public void Tab2Button6()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= 100 && !UpgradeBoxUIList[5].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[5].UIUpdate();
            DataManager.Instance.Jewel -= 100;
            DataManager.Instance.Increased_Attack_Damage += 1;
            DataManager.Instance.UpgradeComplete(5);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    public void Tab2Button7()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= 100 && !UpgradeBoxUIList[6].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[6].UIUpdate();
            DataManager.Instance.Jewel -= 100;
            DataManager.Instance.Critical_Chance += 30;
            DataManager.Instance.UpgradeComplete(6);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    public void Tab2Button8()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= 100 && !UpgradeBoxUIList[7].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[7].UIUpdate();
            DataManager.Instance.Jewel -= 100;
            DataManager.Instance.Critical_Damage += 1;
            DataManager.Instance.UpgradeComplete(7);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    public void Tab2Button9()   
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        if (DataManager.Instance.Jewel >= 100 && !UpgradeBoxUIList[8].IsPurchase)
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
            UIDisplay.Instance.NotiUI(notiColorPurchased, notiTextPurchased);
            UpgradeBoxUIList[8].UIUpdate();
            DataManager.Instance.Jewel -= 100;
            DataManager.Instance.Gold_Gained += 1;
            DataManager.Instance.UpgradeComplete(8);
        }
        else
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorJewel, notiTextJewel);
        }
    }

    public void BattleButton()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        UIDisplay.Instance.BattleButtonClick();
    }

    public void GameButton()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        PlayerPrefs.SetInt("GameMode", 1);
        PlayerPrefs.SetInt("BossMode", 0);
        SceneChange.Instance.SceneLoad("Battle");
    }

    public void BossButton()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        PlayerPrefs.SetInt("GameMode", 0);
        PlayerPrefs.SetInt("BossMode", 1);
        SceneChange.Instance.SceneLoad("Battle");
    }

    public void ResultPopupButton()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        SceneChange.Instance.SceneLoad("Main");
    }

    public void GetNewWeaponPopupButton()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        getNewWeaponPopup.SetActive(false);
    }
}
