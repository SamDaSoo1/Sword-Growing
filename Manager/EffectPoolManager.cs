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
            // ���� ����Ʈ�� �ϳ��̻� ����ִٸ�
            foreach (GameObject effect in sizePools[swordLevel])
            {
                if (effect == null)
                {
                    continue;
                }
                   
                if(effect.activeSelf == false)
                {
                    // ��Ȱ��ȭ�� ����Ʈ�� �����Ѵٸ� �Ҵ��ϰ� ����Ʈ ����
                    _effect = effect; 
                    _effect.transform.position = swordPos;
                    _effect.SetActive(true);
                    return;
                }
            }
        }

        if (_effect == null)
        {
            // ������ ����Ʈ�� ã�����ߴٸ� �˸��� ������� ����Ʈ �����ϰ� ����Ʈ�� ����
            _effect = Instantiate(combineEffect, transform);
            _effect.transform.localScale = Vector3.one * size[swordLevel];
            _effect.transform.position = swordPos;
            sizePools[swordLevel].Add(_effect);
        }
    }
}
