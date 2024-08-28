using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EffectPoolManager : MonoBehaviour
{
    GameObject combineEffect;

    static EffectPoolManager instance;
    public static EffectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EffectPoolManager>();
                if (instance == null)
                {
                    GameObject uIDisplay = new GameObject();
                    instance = uIDisplay.AddComponent<EffectPoolManager>();
                    uIDisplay.name = "@" + typeof(EffectPoolManager).Name;
                    DontDestroyOnLoad(uIDisplay);
                }
            }

            return instance;
        }
    }

    List<List<GameObject>> sizePools = new()
    {
        new() { }, new() { }, new() { }, new() { },
        new() { }, new() { }, new() { }, new() { },
        new() { }, new() { }, new() { }, new() { },
        new() { }, new() { }, new() { }, new() { }
    };

    List<int> size = new List<int>() { 84, 88, 108, 124, 132, 140, 164, 172, 176, 184, 216, 236, 236, 236, 244, 268 };

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        combineEffect = Resources.Load<GameObject>("Prefabs/CombineEffect");
    }

    public void PlayEffect(int swordLevel, Vector3 swordPos)
    {
        GameObject _effect = null;

        if (sizePools[swordLevel].Count > 0)
        {
            // 만약 리스트에 하나이상 들어있다면
            foreach (GameObject effect in sizePools[swordLevel])
            {
                if (effect == null)
                {
                    continue;
                }
                   
                if(effect.activeSelf == false)
                {
                    // 비활성화된 이펙트가 존재한다면 할당하고 이펙트 실행
                    _effect = effect; 
                    _effect.transform.position = swordPos;
                    _effect.SetActive(true);
                    return;
                }
            }
        }

        if (_effect == null)
        {
            // 적절한 이펙트를 찾지못했다면 알맞은 사이즈로 이펙트 생성하고 리스트에 저장
            _effect = Instantiate(combineEffect, transform);
            _effect.transform.localScale = Vector3.one * size[swordLevel];
            _effect.transform.position = swordPos;
            sizePools[swordLevel].Add(_effect);
        }
    }
}
