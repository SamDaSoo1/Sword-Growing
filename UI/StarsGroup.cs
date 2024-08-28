using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class StarsGroup : MonoBehaviour
{
    [SerializeField]
    List<RectTransform> list;

    List<GameObject> twoItemList;

    string maxLevel = "15";

    void Awake()
    {
        list = new List<RectTransform>();
        twoItemList = new List<GameObject>();
    }

    public List<RectTransform> GetChild()
    {
        list = transform.GetComponentsInChildren<RectTransform>()
                        .Where(component => component.gameObject != gameObject)
                        .OrderByDescending(component => int.Parse(component.gameObject.name))
                        .ToList();
        return list;
    }

    // 같은 등급 2개 찾는 함수
    public List<GameObject> PickTwoObj()
    {
        twoItemList = new List<GameObject>();

        list = transform.GetComponentsInChildren<RectTransform>()
                        .Where(component => component.gameObject != gameObject)
                        .OrderBy(component => int.Parse(component.gameObject.name))
                        .ToList();

        for(int i = 0; i < list.Count - 1; i++)
        {
            if (list[i].gameObject.name != maxLevel && list[i].gameObject.name == list[i + 1].gameObject.name)
            {
                twoItemList.Add(list[i].gameObject);
                twoItemList.Add(list[i + 1].gameObject);
                break;
            }
        }

        return twoItemList;
    }

    public GameObject PickOneObj()
    {
        list = transform.GetComponentsInChildren<RectTransform>()
                        .Where(component => component.gameObject != gameObject)
                        .OrderBy(component => int.Parse(component.gameObject.name))
                        .ToList();

        if (CheckAllMaxLevel()) { return null; }

        GameObject pickOne = null;

        while(true)
        {
            int num = Random.Range(0, list.Count);
            if (list[num].gameObject.name != maxLevel)
            {
                pickOne = list[num].gameObject;
                break;
            }
        }

        return pickOne;
    }

    bool CheckAllMaxLevel()
    {
        // 리스트에 들어있는 것들이 전부 만렙인지 확인
        return list.All(x => x.name == maxLevel);
    }
}
