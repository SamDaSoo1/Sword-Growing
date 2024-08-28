using System.Collections;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    ThrowingStarsMake throwingStarsMake;
    Sort sort;

    void Start()
    {
        StartCoroutine(MainBgm());
        throwingStarsMake = FindObjectOfType<ThrowingStarsMake>();
        sort = FindObjectOfType<Sort>();
        Init();
    }

    void Init()
    {
        throwingStarsMake.Init();
        sort.Init();
    }

    IEnumerator MainBgm()
    {
        SoundManager.Instance.StopBGM();
        yield return new WaitForSeconds(1.8f);
        SoundManager.Instance.PlayBGM(BgmSound.MainBGM);
    }
}
