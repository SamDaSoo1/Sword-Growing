using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ThrowingStarsMakeUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp;
    Image image2;
    int makeCoolTime;

    readonly int totalCount = 10;
    public int CurrentCount { get; private set; }

    bool isCoroutineExecute = false;
    private const string LastChargeTimeKey = "LastChargeTime"; // PlayerPrefs Ű

    Coroutine co;
    Stopwatch sw = new Stopwatch();

    void Awake()
    {
        image2 = GetComponent<Image>();
        image2.fillAmount = 1f;

        if (PlayerPrefs.HasKey("Count"))
            CurrentCount = PlayerPrefs.GetInt("Count");
        else
            CurrentCount = totalCount;
    }

    void Start()
    {
        makeCoolTime = DataManager.Instance.Star_Make_CoolTime;
        ThrowingStarsMakeButton.ClickEvent -= Charge;
        ThrowingStarsMakeButton.ClickEvent += Charge;
        DataManager.Instance.OnStarMakeCoolTimed -= UpdateCoolTime;
        DataManager.Instance.OnStarMakeCoolTimed += UpdateCoolTime;

        if(PlayerPrefs.HasKey(LastChargeTimeKey))
        {
            long lastTime = long.Parse(PlayerPrefs.GetString(LastChargeTimeKey));
            long currentTime = DateTime.UtcNow.Ticks;
            long elapsedTicks = currentTime - lastTime;
            int elapsedSeconds = (int)(elapsedTicks / 10000000);
            int chargeCount = elapsedSeconds / DataManager.Instance.Star_Make_CoolTime;
            CurrentCount = Mathf.Min(CurrentCount + chargeCount, totalCount);
            TextUpdate();
        }

        if (CurrentCount < 10)
        {
            isCoroutineExecute = true;
            co = StartCoroutine(ChargeCo());
        }
    }

    private void OnDestroy()
    {
        // �̰͵� �ּ�ó���ϰ� �����ϸ� 15���� ���� ��Ȳ �ùķ��̼� ����
        ThrowingStarsMakeButton.ClickEvent -= Charge;
        if(DataManager.Instance != null)
        {
            DataManager.Instance.OnStarMakeCoolTimed -= UpdateCoolTime;
        }
    }

    void UpdateCoolTime()
    {
        makeCoolTime -= 1;
    }

    public void Charge()
    {
        CurrentCount -= 1;
        DataManager.Instance.TotalCount += 1;
        TextUpdate();
        if (!isCoroutineExecute)
        {
            isCoroutineExecute = true;

            if (co == null) 
            { 
                co = StartCoroutine(ChargeCo()); 
            }
        }
    }

    IEnumerator ChargeCo()
    {
        image2.fillAmount = 0f;
        float time = 0f;
        while(time < makeCoolTime)
        {
            yield return null;
            time += Time.deltaTime;
            image2.fillAmount = time / makeCoolTime;
        }
        image2.fillAmount = 1;
        CurrentCount += 1;
        TextUpdate();
        if (totalCount - CurrentCount > 0)
        {
            co = StartCoroutine(ChargeCo());
        }
        else
        {
            isCoroutineExecute = false;
            co = null;
            yield break;
        }
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            // ��׶����� ���
            sw.Restart();
        }
        else
        {
            // ��׶��� �ƴ� ���
            sw.Stop();
            print($"{sw.ElapsedMilliseconds}ms���� ���");
            CurrentCount += (int)sw.ElapsedMilliseconds / DataManager.Instance.Star_Make_CoolTime;
            if (CurrentCount >= 10)
            {
                CurrentCount = 10;
                TextUpdate();
                image2.fillAmount = 1;
                isCoroutineExecute = false;
                if (co != null)
                {
                    StopCoroutine(co);
                    co = null;
                }
            }

            TextUpdate();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Count", CurrentCount);
        PlayerPrefs.SetString(LastChargeTimeKey, DateTime.UtcNow.Ticks.ToString());
    }

    void TextUpdate()
    {
        tmp.text = $"�� ����\n({CurrentCount} / {totalCount})";
    }
}
