using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowingStarsMakeButton : MonoBehaviour
{
    [SerializeField] ThrowingStarsMakeUI throwingStarsMakeUI;
    [SerializeField] TextMeshProUGUI maximumStarCountText;
    public static event Action ClickEvent;

    int makeCoolTime;

    Color notiColor = new Color(98 / 255f, 79 / 255f, 56 / 255f);
    string notiText1 = "제작된 검이 없습니다.";
    string notiText2 = "검이 꽉 찼습니다.";

    private void Start()
    {
        makeCoolTime = DataManager.Instance.Star_Make_CoolTime;
        DataManager.Instance.OnStarMakeCoolTimed += UpdateCoolTime;
    }

    private void OnDestroy()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.OnStarMakeCoolTimed -= UpdateCoolTime;
        }
    }

    void UpdateCoolTime()
    {
        makeCoolTime -= 1;
    }

    Coroutine co;

    public void Click()
    {
        SoundManager.Instance.PlaySFX(Sfx.Button);
        ClickInternal(false);
    }

    public void Click(bool isAuto)
    {
        ClickInternal(isAuto);
    }

    void ClickInternal(bool isAuto)
    {
        if (throwingStarsMakeUI.CurrentCount > 0 && FullCount())
        {
            SoundManager.Instance.PlaySFX(Sfx.SwordCreate, 0.15f);
            ClickEvent?.Invoke();
        }  
        else if(throwingStarsMakeUI.CurrentCount <= 0)
        {
            UIDisplay.Instance.NotiUI(notiColor, notiText1);
        }
        else if(!FullCount())
        {
            if(!isAuto)
                UIDisplay.Instance.NotiUI(notiColor, notiText2);

            if(co != null)
            {
                StopCoroutine(co);
                co = null;
            }
            co = StartCoroutine(FlashText());
        }
    }

    IEnumerator FlashText()
    {
        maximumStarCountText.color = Color.red;
        yield return new WaitForSeconds(0.125f);
        maximumStarCountText.color = Color.white;
        yield return new WaitForSeconds(0.125f);
        maximumStarCountText.color = Color.red;
        yield return new WaitForSeconds(0.125f);
        maximumStarCountText.color = Color.white;
    }

    bool FullCount()
    {
        return DataManager.Instance.TotalCount < DataManager.Instance.Maximum_Star_Count;
    }
}
