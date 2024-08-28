using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarUpgradeBoxUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lv;
    [SerializeField] TextMeshProUGUI power;
    [SerializeField] TextMeshProUGUI percent;
    [SerializeField] TextMeshProUGUI gold;
    [SerializeField] int cost;
    [SerializeField] Button button;

    int level = 1;
    int levelMax = 5;
    int upgradeSuccessChance = 100;

    Color notiColorGold = new Color(1, 158 / 255f, 60 / 255f);
    string notiTextGold = "��尡 �����մϴ�.";
    Color notiColorSuccessed = new Color(35/255f, 134 / 255f, 1);
    string notiTextSuccessed = "��ȭ ����";
    Color notiColorFailed = new Color(1, 45 / 255f, 47 / 255f);
    string notiTextFailed = "��ȭ ����";

    public void Init(int starGrade, int lev)
    {
        level = lev;
        if (level == levelMax)
        {
            lv.text = "Lv MAX";
            power.text = "���ݷ� : " + (level * starGrade).ToString();
            percent.text = "�ְ���";
            gold.text = 0.ToString();
            button.interactable = false;
            return;
        }

        lv.text = "Lv " + level.ToString();
        power.text = "���ݷ� : " + (level * starGrade).ToString();
        percent.text = "��ȭ " + "(" + (100 - 25 * (level - 1)).ToString() + "%)";
        gold.text = ((int)starGrade * level * 100).ToString();
    }

    public void Set(int starGrade)
    {
        if (DataManager.Instance.Gold < cost * level) 
        {
            SoundManager.Instance.PlaySFX(Sfx.PurchaseFailed);
            UIDisplay.Instance.NotiUI(notiColorGold, notiTextGold);
            return; 
        }

        SoundManager.Instance.PlaySFX(Sfx.PurchaseSuccessed);
        DataManager.Instance.Gold -= cost * level;

        upgradeSuccessChance = 100 - (25 * (level - 1));
        int num = Random.Range(1, 101);

        if (num > upgradeSuccessChance)
        {
            UIDisplay.Instance.NotiUI(notiColorFailed, notiTextFailed);
            return;
        }

        UIDisplay.Instance.NotiUI(notiColorSuccessed, notiTextSuccessed);
        level++;
        DataManager.Instance.StarUpgrade(starGrade);

        if(level == levelMax)
        {
            lv.text = "Lv MAX";
            power.text = "���ݷ� : " + (level * starGrade).ToString();
            percent.text = "�ְ���";
            gold.text = 0.ToString();
            button.interactable = false;
            return;
        }

        lv.text = "Lv " + level.ToString();
        power.text = "���ݷ� : " + (level * starGrade).ToString();
        percent.text = "��ȭ " + "(" + (100 - 25 * (level - 1)).ToString() + "%)";
        gold.text = (starGrade * level * 100).ToString();
    }
}
