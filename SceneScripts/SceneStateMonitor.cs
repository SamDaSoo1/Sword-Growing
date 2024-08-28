using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateMonitor : MonoBehaviour
{
    [SerializeField] GameObject gameMode;
    [SerializeField] GameObject bossMode;

    static BattleResult battleResult;

    static int count;
    public static int Count
    {
        get { return count; }
        set 
        {
            count = value;
            if(count == 0)
            {
                // 씬 전환 함수 실행
                //SceneChange.Instance.SceneLoad("Main");
                battleResult.ShowPopUp(Result.AllStarsThrown);
            }
        }
    }

    void Awake()
    {
        battleResult = FindObjectOfType<BattleResult>();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("GameMode") == 1 && PlayerPrefs.GetInt("BossMode") == 0)
        {
            StartCoroutine(MiniGameBgm());
            gameMode.SetActive(true);
            bossMode.SetActive(false);
            battleResult.GameMode = true;
        }
        else if(PlayerPrefs.GetInt("GameMode") == 0 && PlayerPrefs.GetInt("BossMode") == 1)
        {
            StartCoroutine(BossBgm());
            gameMode.SetActive(false);
            bossMode.SetActive(true);
            battleResult.GameMode = false;
        }
    }

    IEnumerator MiniGameBgm()
    {
        SoundManager.Instance.StopBGM();
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.PlayBGM(BgmSound.MinigameBGM);
    }

    IEnumerator BossBgm()
    {
        SoundManager.Instance.StopBGM();
        yield return new WaitForSeconds(2f);
        while(true)
        {
            SoundManager.Instance.PlayBGM(BgmSound.BossBGM1);
            yield return new WaitForSeconds(SoundManager.Instance.BGMLength(BgmSound.BossBGM1));
            SoundManager.Instance.PlayBGM(BgmSound.BossBGM2);
            yield return new WaitForSeconds(SoundManager.Instance.BGMLength(BgmSound.BossBGM2));
        }
    }
}
